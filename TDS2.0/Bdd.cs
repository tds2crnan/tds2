using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Core
{


    public class Bdd
    {
        MySql.Data.MySqlClient.MySqlConnection conn;

        
        private Bdd(string cmd)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = cmd;
        }

        private static Bdd instanceTds2 = null;
        public static Bdd InstanceTds2
        {
            get
            {
                if (instanceTds2 == null)
                {
                    instanceTds2 = new Bdd("Database=tds2.0;Data Source=localhost;User Id=root;Password=root;Convert Zero Datetime=True");
                }
                return instanceTds2;
            }
        }

        private static Bdd instanceGestRep = null;
        public static Bdd InstanceGestRep
        {
            get
            {
                if (instanceGestRep == null)
                {
                    instanceGestRep = new Bdd("Database=tds2.0;Data Source=localhost;User Id=root;Password=root;Convert Zero Datetime=True");
                }
                return instanceGestRep;
            }
        }

        //public delegate T buildObjetCreate<out T>(Dictionary<string, object> param);
        //public T create<T>(string sql, Dictionary<string, object> param,buildObjetCreate<T> build)
        //    where T: IDaoObjet
        //{
        //    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
        //    T objet = default(T);
        //    try
        //    {
        //        conn.Open();
        //        cmd.Connection = conn;

        //        cmd.CommandText = sql + ";select LAST_INSERT_ID();";
        //        cmd.Prepare();
        //        if (param != null)
        //            foreach (KeyValuePair<string, object> pair in param)
        //                cmd.Parameters.AddWithValue(pair.Key, pair.Value);

        //        param["@id"] = cmd.ExecuteScalar();

        //        objet = build(param);
        //    }
        //    catch (MySqlException ex)
        //    {
        //        MessageBox.Show("Error SQL has occurred: " + ex.Message +"\n"+ex.InnerException.Message,
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return objet;
        //}
        public int create(string sql, Dictionary<string, object> param)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            int id=0;
            try
            {
                if( conn.State == System.Data.ConnectionState.Closed )
                    conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = sql + ";select LAST_INSERT_ID();";
                cmd.Prepare();
                if (param != null)
                    foreach (KeyValuePair<string, object> pair in param)
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);

                object o = cmd.ExecuteScalar();
                id = Convert.ToInt32(o);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //conn.Close();
            }
            return id;
        }

        public delegate T buildObjetScalar<out T>(object objet);
        public T selectScalar<T>(string sql, Dictionary<string, object> param, buildObjetScalar<T> build)
            where T : IDaoObjet
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            T objet = default(T);
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.Prepare();
                if (param != null)
                    foreach (KeyValuePair<string, object> pair in param)
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);

                objet = build( cmd.ExecuteScalar() );
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message + "\n" + ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //conn.Close();
            }
            return objet;
        }

        public delegate T buildObjetSelect<out T>(Dictionary<string, object> row);
        public List<T> select<T>(string sql, Dictionary<string, object> param, buildObjetSelect<T> build)
            where T: IDaoObjet
        {
            List<T> liste = new List<T>();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.Prepare();
                if (param != null)
                    foreach (KeyValuePair<string, object> pair in param)
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                MySqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    
                    while (reader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        rows.Add(row);
                    }                    
                }
                finally
                {
                    reader.Close();
                }
                foreach( Dictionary<string, object> row in rows )
                {
                    liste.Add(build(row));
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message +"\n"+ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message + "\n" + ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //conn.Close();
            }

            return liste;
        }
        public T selectOne<T>(string sql, Dictionary<string, object> param, buildObjetSelect<T> build)
            where T : IDaoObjet
        {
            List<T> liste = new List<T>();

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            T objet = default(T);
            
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.Prepare();
                if (param != null)
                    foreach (KeyValuePair<string, object> pair in param)
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);

                MySqlDataReader reader = cmd.ExecuteReader();
                Dictionary<string, object> row = null;
                try
                {
                    if (reader.Read())
                    {
                        row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
                if( row != null)
                    objet = build(row);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message + "\n" + ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message + "\n" + ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //conn.Close();
            }

            return objet;
        }
        public void update(string sql, Dictionary<string, object> param)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = sql;
                cmd.Prepare();
                if (param != null)
                    foreach (KeyValuePair<string, object> pair in param)
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);

                int result = cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error SQL has occurred: " + ex.Message + "\n" + ex.InnerException.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //conn.Close();
            }

        }
    }
}
