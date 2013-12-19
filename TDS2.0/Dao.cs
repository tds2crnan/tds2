using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Runtime.Caching;

namespace Core
{
    //public abstract class DaoCaching
    //{
    //    static ObjectCache cache = MemoryCache.Default;
    //    protected static T buildSelect<T>(Dictionary<string, object> row)
    //    {
    //        string clef = getClef<T>(row);
    //        if (!cache.Contains(clef))
    //        {
    //            T agent = newObject<T>(row);
    //            cache.Add(clef, agent, new CacheItemPolicy());
    //        }
    //        return (T)cache.Get(clef);
    //    }
    //    protected abstract static string getClef<T>(Dictionary<string, object> row);
    //    protected abstract static T newObject<T>(Dictionary<string, object> row);
    //}

    public class IDaoEntity<T>
        where T : IDaoObjet, new()
    {
        static ObjectCache cache = MemoryCache.Default;

        protected static T buildSelect(Dictionary<string, object> row)
        {
            string clef = typeof(T).ToString() +"->"+ row["id"];
            if (!cache.Contains(clef))
            {
                T agent = new T();
                agent.loadFromBdd(row);
                cache.Add(clef, agent, new CacheItemPolicy());
            }
            return (T)cache.Get(clef);
        }
    }

    public class IDaoFactory<J>
        where J : IDaoObjet
    {
        static ObjectCache cache = MemoryCache.Default;

        protected static T buildSelect<T>(Dictionary<string, object> row)
            where T : J
        {
            string clef = (string)row["nomFactory"] + "->" + row["id"];
            if (!cache.Contains(clef))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                T vacation = (T)assembly.CreateInstance((string)row["nomFactory"]);
                if (vacation == null)
                    throw new Exception("la classe : \"" + (string)row["nomFactory"]+"\" nexiste pas dans le programme");
                vacation.loadFromBdd(row);
                cache.Add(clef, vacation, new CacheItemPolicy());
            }
            return (T)cache.Get(clef);
        }
    }

    public abstract class  IDaoObjet
    {
        int id;
        public int Id
        {
            get { return (int)id; }
        }

        public void loadFromBdd(Dictionary<string, object> row)
        {
            this.id = (int)row["id"];
            actionLoadFromBdd(row);
        }
        public delegate void delegateLoadFromBdd(Dictionary<string, object> row);
        public delegateLoadFromBdd actionLoadFromBdd;

        public Dictionary<string, Object> saveToBdd()
        {
            Dictionary<string, Object> param = new Dictionary<string, object>();
            param["@id"] = this.id;
            actionSaveToBdd(param);
            return param;
        }
        public delegate void delegateSaveToBdd(Dictionary<string, Object> param);
        public delegateSaveToBdd actionSaveToBdd;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if ( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public abstract class IDaoObjetFactory : IDaoObjet
    {
        public IDaoObjetFactory()
        {
            this.actionSaveToBdd += saveToBdd;
            this.actionLoadFromBdd += loadFrBdd;
        }

        //private string tag = ""; //TODO supprimer le tag pas besoin
        public static T loadTag<T>(Dictionary<string, object> row)
        {
            StringReader ss = new StringReader((string)row["tag"]);
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)x.Deserialize(ss);
        }
        public static void saveTag<T>(Dictionary<string, object> param, T objet)
        {
            StringWriter ss = new StringWriter();
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(objet.GetType());
            x.Serialize(ss, objet);
            param["@tag"] = ss.ToString();
        }

        private void loadFrBdd(Dictionary<string, object> row)
        {
            //DBNull test = row["tag"] as System.DBNull;
            //if( test == null )
            //    this.tag = (string)row["tag"];
        }
        private void saveToBdd(Dictionary<string, Object> param)
        {
            param["@nomFactory"] = this.GetType().ToString();
            param["@tag"] = "";
        }
    }

    class ExchangeDao
    {
        //Dictionary<string, Object> param;
        //public void set<T>(string nom, T val)
        //{
        //    param[nom] = val;
        //}
        //public void set<T>(string nom, T val)
        //    where T : IDaoObjet
        //{
        //    if( val != null )
        //        param[nom] = val.Id;
        //}
        //public T get<T>(string nom)
        //    where T : IDaoObjet
        //{
        //    if (param.ContainsKey(nom))
        //    {
        //        object val = param[nom];
        //        if (val.GetType() == typeof(int))
        //        {
        //            int id = (int)val;
        //        }
        //    }
        //    return default(T);
        //}
        //public T get<T>(string nom)
        //{
        //    if (param.ContainsKey(nom))
        //    {
        //        object val = param[nom];
        //        if( val.GetType() == typeof(T) )
        //            return (T)val;
        //    }
        //    return default(T);
        //}
    }

}
