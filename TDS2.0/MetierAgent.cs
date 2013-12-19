using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Core
{


    public class DaoAgent : IDaoEntity<MetierAgent>
    {
        public static MetierAgent create(string nom)
        {
            MetierAgent prototype = new MetierAgent();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@nom"] = nom;
            int id = Bdd.InstanceGestRep.create("insert into agents(nom) values(@nom)", param);
            return findOne(id);
        }
        public static MetierAgent findOne(int id)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceGestRep.selectOne<MetierAgent>("select * from agents where id=@id",param,buildSelect);
        }
        public static List<MetierAgent> findAll()
        {
            return Bdd.InstanceGestRep.select<MetierAgent>("select * from agents", null, buildSelect);
        }
        public static List<MetierAgent> find(string nom)
        {
            Dictionary<string,Object> param = new Dictionary<string,object>();
            param["@nom"] = nom;
            return Bdd.InstanceGestRep.select<MetierAgent>("select * from agents where nom=@nom", param,buildSelect);
        }
        public static void update(MetierAgent agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("update agents set nom=@nom where id=@id", param);
        }
        public static void delete(MetierAgent agent)
        {
            Dictionary<string, Object> param = agent.saveToBdd();
            Bdd.InstanceGestRep.update("delete from agents where id=@id", param);
        }
        public static List<MetierAgent> find(IVacation vacation)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@date"] = String.Format("{0:yyyy-MM-dd}", vacation.Date);
            param["@idType"] = vacation.Type.Id;
            return Bdd.InstanceGestRep.select<MetierAgent>(@"
                select
		            agents.id as id, agents.nom as nom 
	            from
		            agents  left join carrieres on( agents.id = carrieres.idAgent)
                            left join subs on (carrieres.idSub = subs.id) 
                            left join cycles on ( subs.id = cycles.idSub) 
                            left join types on ( cycles.id = types.idCycle)
	            where
		            types.id = @idType and
		            (	(carrieres.dateDebut < @date and carrieres.dateFin > @date) or
		             	(carrieres.dateDebut < @date and carrieres.dateFin > @date) 
		            )	
	            ;", param, buildSelect);
        }
    }

    public class MetierAgent : IDaoObjet
    {       
        string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        //public string prenom="test";
        //public string tel="0152424251";

        public MetierAgent()
        {
            this.actionSaveToBdd += saveToBdd;
            this.actionLoadFromBdd += loadFrBdd;
        }

        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.nom = (string)row["nom"];
            //this.prenom = data.GetString("prenom");
            //this.tel = data.GetString("tel");
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            param["@nom"] = this.nom;
            //param["@prenom"] = this.prenom;
            //param["@tel"] = this.tel;
        }
    }
}
