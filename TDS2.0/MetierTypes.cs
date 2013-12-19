using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class DaoTypes : IDaoFactory<ITypeVacation>
    {
        public static T create<T>(T type)
           where T : ITypeVacation, new()
        {
            Dictionary<string, Object> param = type.saveToBdd();
            int id = Bdd.InstanceTds2.create("insert into types(nomFactory,idCycle,tag) values(@nomFactory,@idCycle,@tag)", param);
            //vacation.Id = id;
            return type;
        }
        public static List<T> findAll<T>()
            where T : ITypeVacation
        {
            return Bdd.InstanceTds2.select<T>("select * from types", null, buildSelect<T>);
        }
        public static T findOne<T>(int id)
            where T : ITypeVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceTds2.selectOne<T>("select * from types where id=@id", param, buildSelect<T>);
        }
        public static T findOne<T>(ICycle cycle)
            where T : ITypeVacation
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@idCycle"] = cycle.Id;
            param["@nomFactory"] = typeof(T);
            return Bdd.InstanceTds2.selectOne<T>("select * from types where idCycle=@idCycle and nomFactory=@nomFactory", param, buildSelect<T>);
        }

        public static void update<T>(T objet)
            where T : ITypeVacation
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("update types set nomFactory=@nomFactory, idCycle=@idCycle, tag=@tag where id=@id", objet.saveToBdd());
        }
        public static void delete<T>(T objet)
            where T : ITypeVacation
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("delete from types where id=@id", param);
        }
    }
    public abstract class ITypeVacation : IDaoObjetFactory
    {
        public ICycle cycle;
        public ICycle Cycle
        {
            set
            {
                if (cycle != value)
                {
                    cycle = value;
                    DaoTypes.update(this);
                }
            }
            get
            {
                return cycle;
            }
        }
        public ITypeVacation()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }

        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.cycle = DaoCycle.findOne<ICycle>((int)row["idCycle"]);
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            if (this.cycle != null)
                param["@idAgent"] = this.cycle.Id;
        }
        public abstract string getNom();
        //public abstract ICycle getCycle();
        //public abstract DateTime debut();
        //public abstract DateTime fin();
        public abstract IViewCellSemaineSub makeView(PresenterSemaineSub presenter, DateTime date);
        public abstract string makePrint();
    }
}
