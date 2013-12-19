using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    class DaoEquip : IDaoEntity<MetierEquip>
    {
        public static MetierEquip create(string nom, DateTime date)
        {
            MetierEquip prototype = new MetierEquip();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@nom"] = nom;
            param["@dateStart"] = String.Format("{0:yyyy-MM-dd}", date);
            int id = Bdd.InstanceGestRep.create("insert into equips(nom,dateStart) values(@nom,@dateStart)", param);
            return findOne(id);
        }
        public static MetierEquip findOne(int id)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceGestRep.selectOne<MetierEquip>("select * from equips where id=@id", param, buildSelect);
        }
        public static List<MetierEquip> findAll()
        {
            return Bdd.InstanceGestRep.select<MetierEquip>("select * from equips", null, buildSelect);
        }
        public static void update(MetierEquip agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("update equips set nom=@nom, dateStart=@dateStart where id=@id", param);
        }
        public static void delete(MetierEquip agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("delete from equips where id=@id", param);
        }
        public static List<MetierEquip> find(MetierSub sub, DateTime date)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@idSub"] = sub.Id;
            param["@date"] = String.Format("{0:yyyy-MM-dd}", date);
            return Bdd.InstanceGestRep.select<MetierEquip>(@"
                select 
		            equips.id as id, equips.nom as nom, equips.dateStart as dateStart
	            from
		            equips	left join ce on ( equips.id = ce.idEquip )
				            left join cycles on ( ce.idCycle = cycles.id )
	            where
		            cycles.idSub = @idSub and
		            (	(cycles.dateDebut < @date and cycles.dateFin > @date) or
		 	            (cycles.dateDebut < @date and cycles.dateFin > @date) 
		            )	
	            ;", param, buildSelect);
        }
        public static List<MetierEquip> find(DateTime date)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = date.Year;//String.Format("{0:yyyy-MM-dd}", date);
            return Bdd.InstanceGestRep.select<MetierEquip>(@"
                    select 
	                    equips.id as id, equips.nom as nom, equips.dateStart as dateStart
                    from
	                    equips	left join ce on ( equips.id = ce.idEquip )
			                    left join cycles on ( ce.idCycle = cycles.id )
                    where
	                    EXTRACT(YEAR FROM cycles.dateDebut) <= 2013 and 
	                    EXTRACT(YEAR FROM cycles.dateFin) >= 2013
	            ;", param, buildSelect);
        }
    }

    public class MetierEquip : IDaoObjet
    {
        string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        DateTime dateStart;
        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        public MetierEquip()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }

        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.nom = (string)row["nom"];
            this.dateStart = (DateTime)row["dateStart"];
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            param["@nom"] = this.nom;
            param["@dateStart"] = String.Format("{0:yyyy-MM-dd}", this.dateStart);
        }
    }
}
