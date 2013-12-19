using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    class DaoFonction : IDaoEntity<MetierFonction>
    {
        public static MetierFonction create(MetierSub sub, ICycle cycle,DateTime dateDebut, DateTime dateFin)
        {
            MetierFonction prototype = new MetierFonction();
            Dictionary<string, Object> param = prototype.saveToBdd();
            param["@idSub"] = sub;
            param["@cycle"] = cycle;
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", dateFin);
            int id = Bdd.InstanceGestRep.create("insert into fonctions(idSub,idCycle,dateDebut,dateFin) values(@idSub,@cycle,@dateDebut,@dateFin)", param);
            return findOne(id);
        }
        public static MetierFonction findOne(int id)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = id;
            return Bdd.InstanceGestRep.selectOne<MetierFonction>("select * from fonctions where id=@id", param, buildSelect);
        }
        public static List<MetierFonction> findAll()
        {
            return Bdd.InstanceGestRep.select<MetierFonction>("select * from fonctions", null, buildSelect);
        }
        public static void update(MetierFonction sub)
        {
            Dictionary<string, Object> param = sub.saveToBdd();
            Bdd.InstanceGestRep.update("update fonctions set idSub=@idSub, idCycle=@idCycle, dateDebut=@dateDebut, dateFin=@dateFin where id=@id", param);
        }
        public static void delete(MetierFonction sub)
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = sub.Id;
            Bdd.InstanceGestRep.update("delete from fonctions where id=@id", param);
        }
    }

    class MetierFonction : IDaoObjet
    {
        MetierSub sub;
        MetierSub Sub
        {
            get { return sub; }
            set { sub = value; }
        }
        DateTime dateDebut;
        DateTime DateDebut
        {
            get{ return dateDebut; }
            set{dateDebut = value;}
        }
        DateTime dateFin;
        DateTime DateFin
        {
            get { return dateFin; }
            set { dateFin= value; }
        }
        ICycle cycle;
        ICycle Cycle
        {
            set { cycle = value; }
            get { return cycle; }
        }

        public MetierFonction()
        {
            this.actionLoadFromBdd += loadFrBdd;
            this.actionSaveToBdd += saveToBdd;
        }
        private void loadFrBdd(Dictionary<string, object> row)
        {
            this.sub = DaoSub.findOne( (int)row["idSub"] ) ;
            this.cycle = DaoCycle.findOne<ICycle>((int)row["idCycle"]);
            this.dateDebut = (DateTime)row["dateDebut"];
            this.dateFin = (DateTime)row["dateFin"];
        }
        private void saveToBdd(Dictionary<string, object> param)
        {
            if( this.sub != null )
                param["@idSub"] = this.sub.Id;
            if( this.cycle != null )
                param["@idCycle"] = this.cycle.Id;
            param["@dateDebut"] = String.Format("{0:yyyy-MM-dd}", this.dateDebut);
            param["@dateFin"] = String.Format("{0:yyyy-MM-dd}", this.dateFin);
        }
    }
}
