using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Core
{
    class DaoCycle : IDaoFactory<ICycle>
    {
        public static T create<T>(MetierSub sub, DateTime dateDebut, DateTime dateFin)
           where T : ICycle, new()
        {
            T prototype = new T();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@idSub"] = sub.Id;
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", dateFin);
            int id = Bdd.InstanceTds2.create("insert into cycles(dateDebut,dateFin,idSub,nomFactory,tag) values(@dateDebut,@dateFin,@idSub,@nomFactory,@tag)", param);
            return findOne<T>(id);
        }
        public static List<T> findAll<T>()
            where T : ICycle
        {
            return Bdd.InstanceTds2.select<T>("select * from cycles", null, buildSelect<T>);
        }
        public static T findOne<T>(int id)
            where T : ICycle
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceTds2.selectOne<T>("select * from cycles where id=@id", param, buildSelect<T>);
        }
        public static void update<T>(T objet)
            where T : ICycle
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("update cycles set dateDebut=@dateDebut, dateFin=@dateFin, idSub=@idSub, nomFactory=@nomFactory, tag=@tag where id=@id", objet.saveToBdd());
        }
        public static void delete<T>(T objet)
            where T : ICycle
        {
            Dictionary<string, Object> param = objet.saveToBdd();
            Bdd.InstanceGestRep.update("delete from cycles where id=@id", param);
        }
        public static List<T> find<T>(MetierSub sub, DateTime dateDebut, DateTime dateFin)
            where T : ICycle
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", dateFin);
            param["@idSub"] = sub.Id;
            return Bdd.InstanceTds2.select<T>(@"
                select 
                    * 
                from 
                    cycles 
                where 
                    cycles.idSub = @idSub and
                    (   (cycles.dateDebut < @dateFin and cycles.dateFin > @dateFin) or 
                        ( cycles.dateDebut < @dateDebut and cycles.dateFin > @dateDebut) 
                    )
                ;", param, buildSelect<T>);
        }
//        public static List<T> findAnnee<T>(MetierEquip equip, DateTime date)
//            where T : ICycle
//        {
//            Dictionary<string, Object> param = new Dictionary<string, object>();
//            param["@date"] = date.Year; // String.Format("{0:yyyy-MM-dd}", date);
//            param["@idEquip"] = equip.Id;
//            return Bdd.InstanceTds2.select<T>(@"
//                select 
//	                cycles.id as id, cycles.dateDebut as dateDebut, cycles.dateFin as dateFin, cycles.nomFactory as nomFactory, cycles.tag as tag, cycles.idSub as idSub
//                from 
//	                equips  left join ce on ( equips.id = ce.idEquip )
//			                left join cycles on ( ce.idCycle = cycles.id )
//                where 
//	                cycles.idSub = 1 and
//	                EXTRACT(YEAR FROM cycles.dateDebut) <= 2013 and 
//	                EXTRACT(YEAR FROM cycles.dateFin) >= 2013
//                ;", param, buildSelect<T>);
//        }
        public static List<T> findAnnee<T>(MetierSub sub, DateTime date)
            where T : ICycle
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = date.Year; // String.Format("{0:yyyy-MM-dd}", date);
            param["@idSub"] = sub.Id;
            return Bdd.InstanceTds2.select<T>(@"
                select 
	                cycles.id as id, cycles.dateDebut as dateDebut, cycles.dateFin as dateFin, cycles.nomFactory as nomFactory, cycles.tag as tag, cycles.idSub as idSub
                from 
	                equips  left join ce on ( equips.id = ce.idEquip )
			                left join cycles on ( ce.idCycle = cycles.id )
                where 
	                cycles.idSub = @idSub and
	                EXTRACT(YEAR FROM cycles.dateDebut) <= @date and 
	                EXTRACT(YEAR FROM cycles.dateFin) >= @date
                ;", param, buildSelect<T>);
        }
        public static List<T> findAnnee<T>(MetierSub sub, MetierEquip equip, DateTime date)
            where T : ICycle
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = date.Year; // String.Format("{0:yyyy-MM-dd}", date);
            param["@idSub"] = sub.Id;
            param["@idEquip"] = equip.Id;
            return Bdd.InstanceTds2.select<T>(@"
                select 
	                cycles.id as id, cycles.dateDebut as dateDebut, cycles.dateFin as dateFin, cycles.nomFactory as nomFactory, cycles.tag as tag, cycles.idSub as idSub
                from 
	                equips  left join ce on ( equips.id = ce.idEquip )
			                left join cycles on ( ce.idCycle = cycles.id )
                where 
	                cycles.idSub = @idSub and
                    equips.id = @idEquip and
	                EXTRACT(YEAR FROM cycles.dateDebut) <= @date and 
	                EXTRACT(YEAR FROM cycles.dateFin) >= @date
                ;", param, buildSelect<T>);
        }
    }

    public abstract class ICycle : IDaoObjetFactory
    {
        DateTime dateDebut;
        public DateTime DateDebut
        {
            get { return dateDebut; }
        }
        DateTime dateFin;
        public DateTime DateFin
        {
            get { return dateFin; }
        }
        MetierSub sub;
        public MetierSub Sub
        {
            get { return sub; }
        }
        public ICycle()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }
        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.dateDebut = (DateTime)row["dateDebut"];
            this.dateFin = (DateTime)row["dateFin"];
            this.sub = DaoSub.findOne((int)row["idSub"]);
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            param["@dateDebut"] = this.dateDebut;
            param["@dateFin"] = this.dateFin;
            if (this.sub != null)
                param["@idSub"] = this.sub.Id;
        }
        public abstract void peuplerCycle(MetierAgent agent, int iteration, DateTime date);

        //retourne la duree du cycle en nombre de jour
        public abstract int dureeCycle();

        public abstract List<ITypeVacation> getListTypeVacation();
        public abstract UserControl makeView(PresenterPeupler racine, MetierEquip equip, DateTime date);
    }

}
