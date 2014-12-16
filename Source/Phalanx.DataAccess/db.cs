using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Phalanx.Common;

namespace Phalanx.DataAccess
{
    public static class db
    {

        public static SqlQuery ReadXMLQuery(string XMLname)
        {

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SqlQuery));
            var fs = new System.IO.FileStream("..\\..\\Querys\\" + XMLname + ".xml", System.IO.FileMode.Open);
            SqlQuery ret = serializer.Deserialize(fs) as SqlQuery;
            fs.Close();
            return ret;
        }

        public static FoxCursor PesquisaSQL(string QueryName, Condition cond)
        {
            SqlQuery slqQ = new SqlQuery(QueryName);
            FoxCursor ret = new FoxCursor(slqQ, cond);
            return ret;
        }
        public static FoxCursor PesquisaSQL(string QueryName)
        {
            return PesquisaSQL(QueryName, null);
        }

        public static Things ScatterBlank(string tableName)
        {
            Things ret = new Things();
            MyTableSchema sch = GetSchema(tableName);

            foreach (KeyValuePair<string,TypeSize> item in sch)
            {
                if (item.Value.type == typeof(int) || item.Value.type == typeof(Int64))
                    ret[item.Key] = 0;

                if (item.Value.type == typeof(String) )
                    ret[item.Key] = String.Empty;

                if (item.Value.type == typeof(Decimal) || item.Value.type == typeof(Double))
                    ret[item.Key] = Decimal.Parse("0");

                if (item.Value.type == typeof(DateTime))
                    ret[item.Key] = DBNull.Value;

            }
            
          

            return ret;
        }

         public static Object AtuSql(char cAction, string tableName, ref FoxCursor Cursor, String parameters = "")
         {
             Things obj = Cursor.ScatterName();
             Object ret = AtuSql(cAction, tableName, ref obj, parameters);

             if (cAction=='A')
                Cursor.Set(RuntimeParameters.DefaultPrimaryKeyField, ret);

             return ret;
         }

        public static Object AtuSql(char cAction, string tableName, ref Things Cursor, String parameters = "")
        {
            Object ret = new Object();
            MyTableSchema schem = new MyTableSchema(tableName);
            Things line = Cursor;
            Things obj = new Things();

            String PK = RuntimeParameters.DefaultPrimaryKeyField;


            foreach (KeyValuePair<String, Object> item in line)
            {
                if (schem.ContainsKey(item.Key) && !obj.ContainsKey(item.Key))
                    obj.Add(item);
            }


            if (!obj.ContainsKey(PK))
            {
                throw new System.InvalidOperationException(PK + " nao consta no cursor!");
            }

            
            String cCommand = String.Empty;
            String campos = String.Empty, valores = String.Empty;
            switch (cAction.ToString().ToUpper())
            {
                case "A":
                    cCommand = "INSERT INTO " + tableName + "(<campos>) VALUES(<valores>) ";

                    Condition values = new Condition();

                    foreach (KeyValuePair<String, Object> item in obj)
                    {

                        if (parameters.Contains("NOALTOINC") || item.Key != RuntimeParameters.DefaultPrimaryKeyField || schem[RuntimeParameters.DefaultPrimaryKeyField].type == typeof(string) )
                        {
                            campos += ", " + item.Key;
                            valores += ", @" + item.Key;
                            values.Add(item.Key, item.Value);
                        }
                    }

                    cCommand = cCommand.Replace("<campos>", campos.Substring(1));
                    cCommand = cCommand.Replace("<valores>", valores.Substring(1));


                    int nPK;
                    nPK = db.exe(cCommand, values);

                    if (!parameters.Contains("NOALTOINC"))
                    {
                        Cursor[RuntimeParameters.DefaultPrimaryKeyField] = nPK;
                        ret = nPK;
                    }
                    break;
                case "M":
                    cCommand = "UPDATE " + tableName + " SET <campos> WHERE <cond>";
                    
                    Condition upd = new Condition();

                    foreach (KeyValuePair<String, Object> item in obj)
                    {
                        if (item.Key != RuntimeParameters.DefaultPrimaryKeyField)
                        {
                            campos += ", " + item.Key +"=@" + item.Key;
                            upd.Add(item.Key, item.Value);
                        }
                    }

                    cCommand = cCommand.Replace("<campos>", campos.Substring(1));
                    cCommand = cCommand.Replace("<cond>", RuntimeParameters.DefaultPrimaryKeyField+"=@"+RuntimeParameters.DefaultPrimaryKeyField);
                    upd[RuntimeParameters.DefaultPrimaryKeyField] = obj[RuntimeParameters.DefaultPrimaryKeyField];
                    db.exe(cCommand, upd);                    

                    break;

                case "D":
                    cCommand = "DELETE FROM " + tableName + " WHERE " + RuntimeParameters.DefaultPrimaryKeyField + "= @" + RuntimeParameters.DefaultPrimaryKeyField;
                    Condition ID = new Condition();
                    ID[RuntimeParameters.DefaultPrimaryKeyField] = obj[RuntimeParameters.DefaultPrimaryKeyField];
                    db.exe(cCommand, ID);
                    break;
                default:
                    break;
            }


            return ret;
        }

        public static int exe(String command)
        {
            return exe(command, null);
        }

        public static DataTable Query(String command)
        {
            return Query(command, null);
        }

        public static Object ExecuteEscalar(String command, Condition cond)
        {
            DataTable dt = Query(command, cond);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0];
            else
                return null;
        }

        public static Object ExecuteEscalar(String command)
        {
            return ExecuteEscalar(command, null);
        }


        public static int exe(String command, Condition Values)
        {
            String strConn = RuntimeParameters.GetDefaultConnection();
            int Ret = 0;


           
                    using (var conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(command, conn))
                        {

                            if (Values != null)
                                foreach (KeyValuePair<string, Object> item in Values)
                                {
                                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                                }

                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "SELECT @@IDENTITY";
                            try
                            {
                                Decimal ident = (Decimal)cmd.ExecuteScalar();
                                Ret = (int)ident;
                            }
                            catch (Exception)
                            {
                                Ret = -1;
                            }
                        }
                    }
                
              

            
            
            return Ret;

        }
   
        public static DataTable Query(String command, Condition cond)
        {
            var Retorno = new DataTable();
            String strConn = RuntimeParameters.GetDefaultConnection();




            using (var conn = new SqlConnection(strConn))
            {

                conn.Open();
                if (cond == null || cond.Count == 0)
                {
                    if (command.ToUpper().Contains("{COND}"))
                        command = command.ToUpper().Replace("{COND}", "");

                    using (var adap = new SqlDataAdapter(command, conn))
                    {
                        adap.Fill(Retorno);
                    }
                }
                else
                {
                    string cOperation = command.ToUpper().Contains("WHERE") ? " AND " : " WHERE ";

                    if (!command.ToUpper().Contains("{COND}"))
                        command = command + cOperation + " (" + cond.Comando + ")";
                    else
                        command = command.ToUpper().Replace("{COND}", cOperation + " (" + cond.Comando + ")");

                    using (var comm = new SqlCommand(command, conn))
                    {
                        foreach (KeyValuePair<String, Object> item in cond)
                            comm.Parameters.Add(new SqlParameter(item.Key.ToString(), item.Value));

                        using (var adap = new SqlDataAdapter(comm))
                        { adap.Fill(Retorno); }

                    }
                }


            }

            return Retorno;
        }

        public static MyTableSchema GetSchema(String tableName)
        {

            MyTableSchema ret = new MyTableSchema();
            DataTable schema = null;

            String comando = "SELECT * FROM " + tableName + " WHERE 1=0";


           
                using (var conn = new SqlConnection(RuntimeParameters.GetDefaultConnection()))
                {
                    conn.Open();
                    using (SqlDataReader datareader = new SqlCommand(comando, conn).ExecuteReader())
                    {
                        schema = datareader.GetSchemaTable();
                        foreach (DataRow item in schema.Rows)
                        {
                            ret.Add(item["ColumnName"].ToString(), new TypeSize((int)item["ColumnSize"], (Type)item["DataType"]));
                        }
                    }

                }
          


            return ret;

        }



    }

    
    
}
