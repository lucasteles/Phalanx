using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phalanx.Common
{
    public static class RuntimeParameters
    {
        public static DataBases currentDatabase = DataBases.SQLServerLocalDB;

        public static String DefaultPrimaryKeyField = "PK_ID";

     
        

        public static string GetDefaultConnection()
        {
            String con="";

            if (currentDatabase == DataBases.SQLServerLocalDB)
                con = System.Configuration.ConfigurationManager.ConnectionStrings["LocalDBCon"].ConnectionString;
            


            return con;
        }

        public static string GetDefaultIconPath()
        {
            String ret = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) +
                                "\\Icons\\";

            // remove pasta de excução
            ret = ret.Replace("\\bin\\Debug", "");
            ret = ret.Replace("\\bin\\Release", "");

            return ret;

        }



    }
}
