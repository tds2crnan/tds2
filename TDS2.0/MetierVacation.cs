using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.ComponentModel;


namespace Core
{
    public class DaoIVacation : IDaoFactory<IVacation>
    {
        public static T create<T>(T vacation)
           where T : IVacation, new()
        {
            Dictionary<string, Object> param = vacation.saveToBdd();
            int id = Bdd.InstanceTds2.create("insert into vacations(nomFactory,date,idAgent,tag,idType) values(@nomFactory,@date,@idAgent,@tag,@idType)", param);
            //vacation.Id = id;
            return vacation;
        }
        public static T create<T>(MetierAgent agent, DateTime date,ITypeVacation type)
           where T : IVacation, new()
        {
            T prototype = new T();
            Dictionary<string, Object> param = prototype.saveToBdd();
            if (agent != null)
                param["@idAgent"] = agent.Id;
            else
                param["@idAgent"] = DBNull.Value;
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            param["@idType"] = type.Id;
            int id = Bdd.InstanceTds2.create("insert into vacations(nomFactory,date,idAgent,tag,idType) values(@nomFactory,@date,@idAgent,@tag,@idType)", param);
            return findOne<T>(id);
        }
        public static T create<T,X>(MetierAgent agent, DateTime date, ITypeVacation type, X tag)
           where T : IVacation, new()
        {
            T prototype = new T();
            Dictionary<string, Object> param = prototype.saveToBdd();
            if (agent != null)
                param["@idAgent"] = agent.Id;
            else
                param["@idAgent"] = DBNull.Value;
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            param["@idType"] = type.Id;
            IDaoObjetFactory.saveTag(param,tag);
            int id = Bdd.InstanceTds2.create("insert into vacations(nomFactory,date,idAgent,tag,idType) values(@nomFactory,@date,@idAgent,@tag,@idType)", param);
            return findOne<T>(id);
        }
        public static List<T> findAll<T>()
            where T : IVacation
        {            
            return Bdd.InstanceTds2.select<T>("select * from vacations", null,buildSelect<T>);
        }
        public static T findOne<T>(int id)
            where T : IVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceTds2.selectOne<T>("select * from vacations where id=@id", param, buildSelect<T>);
        }
        public static T findOne<T>(DateTime date)
            where T : IVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            param["@nomFactory"] = typeof(T);
            return Bdd.InstanceTds2.selectOne<T>("select * from vacations where date=@date and nomFactory=@nomFactory", param, buildSelect<T>);
        }
        public static List<T> find<T>(DateTime date)
            where T : IVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            param["@nomFactory"] = typeof(T);
            return Bdd.InstanceTds2.select<T>("select * from vacations where date=@date and nomFactory=@nomFactory", param, buildSelect<T>);
        }
        public static List<T> find<T,J>(DateTime date)
            where T : IVacation
            where J : ITypeVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            param["@nomFactoryVacation"] = typeof(T);
            param["@nomFactoryType"] = typeof(J);
            return Bdd.InstanceTds2.select<T>(@"
                select 
                     vacations.id as id, vacations.idAgent as idAgent, vacations.date as date, vacations.nomFactory as nomFactory,
                     vacations.tag as tag, vacations.idType as idType
                from 
                    vacations   left join types on ( vacations.idType = types.id)
                where 
                    vacations.date=@date and 
                    vacations.nomFactory=@nomFactoryVacation and
                    types.nomFactory=@nomFactoryType
            ;", param, buildSelect<T>);
        }
        public static List<T> findVacAnnee<T>(int idAgent, DateTime date)
           where T : IVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = idAgent;
            param["@date"] = date.Year; //String.Format("{0:yyyy-MM-dd}", date);
            return Bdd.InstanceTds2.select<T>("SELECT * FROM vacations WHERE idAgent=@id and EXTRACT(YEAR FROM date)=@date", param, buildSelect<T>);
        }
        public static void update<T>(T objet)
            where T : IVacation
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("update vacations set nomFactory=@nomFactory, date=@date, idAgent=@idAgent, tag=@tag, idType=@idType where id=@id", objet.saveToBdd());
        }
        public static void delete<T>(T objet)
            where T : IVacation
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("delete from vacations where id=@id", param);
        }
    }

    public class IVacation : IDaoObjetFactory
    {
        protected DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
        }
        protected MetierAgent agent;
        public MetierAgent Agent
        {
            set
            {
                if (agent != value)
                {
                    agent = value;
                    NotifyPropertyChanged("agent");
                }
            }
            get
            {
                return agent;
            }
        }
        protected ITypeVacation type;
        public ITypeVacation Type
        {
            //set
            //{
            //    if (type != value)
            //    {
            //        type = value;
            //    }
            //}
            get
            {
                return type;
            }
        }
        public IVacation()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
            this.PropertyChanged += notifyPropertyChanged;
        }
        private void notifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DaoIVacation.update(this);
        }
        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.date = (DateTime)row["date"];
            if (row["idAgent"] != DBNull.Value)
                this.agent = DaoAgent.findOne((int)row["idAgent"]);
            else
                this.agent = null;
            this.type = DaoTypes.findOne<ITypeVacation>((int)row["idType"]);
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            param["@date"] = String.Format("{0:yyyy-MM-dd}", this.date);
            if(this.agent!=null)
                param["@idAgent"] = this.agent.Id;
            if (this.type != null)
                param["@idType"] = this.type.Id;
        }
    }

    //public class Vacation_D2_J1 : IVacation
    //{
    //    int agent_numero;
    //    public int NumeroAgent{
    //        set 
    //        {
    //            agent_numero = value;
    //            DaoIVacation.update(this);
    //        }
    //        get
    //        {
    //            return agent_numero;
    //        }
    //    }
    //    public Vacation_D2_J1()
    //    {
    //        this.actionLoadFromBdd += loadFrBdd;
    //        this.actionSaveToBdd += saveToBdd;
    //    }

    //    private void loadFrBdd(Dictionary<string, object> row)
    //    {
    //        agent_numero = loadTag<int>();
    //    }
    //    private void saveToBdd(Dictionary<string, object> param)
    //    {
    //        saveTag<int>(agent_numero);
    //    }
    //}
}
