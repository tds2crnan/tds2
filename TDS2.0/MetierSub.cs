using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Core
{
    public class DaoSub : IDaoEntity<MetierSub>
    {
        public static MetierSub create(string nom)
        {
            MetierSub prototype = new MetierSub();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@nom"] = nom;
            int id = Bdd.InstanceGestRep.create("insert into subs(nom) values(@nom)", param);
            return findOne(id);
        }
        public static MetierSub findOne(int id)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceGestRep.selectOne<MetierSub>("select * from subs where id=@id", param, buildSelect);
        }
        public static List<MetierSub> findAll()
        {
            return Bdd.InstanceGestRep.select<MetierSub>("select * from subs", null, buildSelect);
        }
        public static List<MetierSub> find(string nom)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@nom"] = nom;
            return Bdd.InstanceGestRep.select<MetierSub>("select * from subs where nom=@nom", param, buildSelect);
        }
        public static void update(MetierSub sub)
        {
            Dictionary<string, Object> param = sub.saveToBdd();
            Bdd.InstanceGestRep.update("update subs set nom=@nom where id=@id", param);
        }
        public static void delete(MetierSub sub)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = sub.Id;
            Bdd.InstanceGestRep.update("delete from subs where id=@id", param);
        }
        public static List<MetierSub> findAnnee(DateTime date, MetierEquip equip)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = date.Year;
            param["@idEquip"] = equip.Id;
            return Bdd.InstanceGestRep.select<MetierSub>(@"
                select
	               subs.nom as nom, subs.id as id 
                from 
	                equips 	left join ce on (equips.id = ce.idEquip)
			                left join cycles on ( ce.idCycle = cycles.id )
			                left join subs on ( cycles.idSub = subs.id )
                where
	                equips.id = @idEquip and
	                EXTRACT(YEAR FROM cycles.dateDebut) <= @date and 
                    EXTRACT(YEAR FROM cycles.dateFin) >= @date
            ;", param, buildSelect);
        }
        public static List<MetierSub> find(DateTime dateDebut, DateTime dateFin)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", dateFin);
            return Bdd.InstanceGestRep.select<MetierSub>(@"
                select 
                    subs.nom as nom, subs.id as id 
                from
                    cycles  left join subs on ( cycles.idSub = subs.id )
                where 
   	                (cycles.dateDebut < @dateFin and cycles.dateFin > @dateFin) or 
		            ( cycles.dateDebut < @dateDebut and cycles.dateFin > @dateDebut) 
            ;", param, buildSelect);
        }
    }

    public class MetierSub : IDaoObjet
    {
        string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public MetierSub()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }
        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.nom = (string)row["nom"];
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            param["@nom"] = this.nom;
        }

        public List<ITypeVacation> getTypeVacationPossible(DateTime date)
        {
            List<ITypeVacation> liste = new List<ITypeVacation>();
            liste.Add(new Type_J1_D1_2013());
            return liste;
        }
    }
}
