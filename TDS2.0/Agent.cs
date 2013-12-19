using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    class DaoAgent
    {
        public static Agent create(string nom)
        {
            Dictionary<string,Object> param = new Dictionary<string,object>();
            param["@nom"] = nom;
            Bdd.Instance.insert("insert into agents(nom) values(@nom)", param);
            return null;
        }
        
    }

    class Agent
    {       
        int id;
        string nom;
    }
}
