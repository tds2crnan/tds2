using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    class DaoICarriere : IDaoFactory<ICarriere>
    {
        public static T create<T>(MetierAgent agent, MetierSub sub, MetierEquip equip, DateTime dateDebut, DateTime dateFin)
            where T : ICarriere, new()
        {
            T prototype = new T();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@idAgent"] = agent.Id;
            param["@idSub"] = sub.Id;
            param["@idEquip"] = equip.Id;
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", dateFin);
            int id = Bdd.InstanceGestRep.create("insert into carrieres(idAgent,idSub,idEquip,dateDebut,dateFin) values(@idAgent,@idSub,@idEquip,@dateDebut,@dateFin)", param);
            return findOne<T>(id);
        }
        public static T findOne<T>(int id)
            where T : ICarriere
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceGestRep.selectOne<T>("select * from carrieres where id=@id", param, buildSelect<T>);
        }
        public static List<T> findAll<T>()
            where T : ICarriere
        {
            return Bdd.InstanceGestRep.select<T>("select * from carrieres", null, buildSelect<T>);
        }
        public static void update(ICarriere agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("update carrieres set idAgent=@idAgent, idSub=@idSub, idEquip=@idEquip, dateDebut=@dateDebut, dateFin=@dateFin where id=@id", param);
        }
        public static void delete(ICarriere agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("delete from carrieres where id=@id", param);
        }
        //
        public static List<T> find<T>(MetierAgent agent)
            where T : ICarriere
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@idAgent"] = agent.Id;
            return Bdd.InstanceGestRep.select<T>("select * from carrieres where idAgent=@idAgent", param, buildSelect<T>);
        }
    }

    public abstract class ICarriere : IDaoObjetFactory
    {
        protected MetierSub sub;
        public MetierSub Sub
        {
            get
            {
                return sub;
            }
            set
            {
                if (value != sub)
                {
                    sub = value;
                    DaoICarriere.update(this);
                }
            }
        }
        protected MetierAgent agent;
        public MetierAgent Agent
        {
            get
            {
                return agent;
            }
            set
            {
                if (value != agent)
                {
                    agent = value;
                    DaoICarriere.update(this);
                }
            }
        }
        protected MetierEquip equip;
        public MetierEquip Equip
        {
            get
            {
                return equip;
            }
            set
            {
                if (value != equip)
                {
                    equip = value;
                    DaoICarriere.update(this);
                }
            }
        }
        protected DateTime dateDebut;
        public DateTime DateDebut
        {
            get
            {
                return dateDebut;
            }
            set
            {
                if (value != dateDebut)
                {
                    dateDebut = value;
                    DaoICarriere.update(this);
                }
            }
        }
        protected DateTime dateFin;
        public DateTime DateFin
        {
            get
            {
                return dateFin;
            }
            set
            {
                if (value != dateFin)
                {
                    dateFin = value;
                    DaoICarriere.update(this);
                }
            }
        }
        public ICarriere()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }

        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.agent = DaoAgent.findOne( (int)row["idAgent"] );
            this.sub = DaoSub.findOne((int)row["idSub"]);
            this.equip = DaoEquip.findOne((int)row["idEquip"]);
            this.dateDebut = (DateTime)row["dateDebut"];
            this.dateFin = (DateTime)row["dateFin"];
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            if (this.agent != null)
                param["@idAgent"] = this.agent.Id;
            if (this.equip != null)
                param["@idEquip"] = this.equip.Id;
            if (this.sub != null)
                param["@idSub"] = this.sub.Id;
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", this.dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", this.dateFin);
        }
        abstract public IViewCellCarriere makeView(PresenterAgent presenter);
    }
}

