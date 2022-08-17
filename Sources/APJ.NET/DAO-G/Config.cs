using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao
{
    internal static class Config
    {
        public static string CONNECTION_STRING
            = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BetSport;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            //= "Data Source = (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = John)(PORT = 1521))" +
            //"(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = XE)));Password=apj#;User ID=apj#";
        public static string DB_NAME
            = "SQL_SERVER";
        //= "ORACLE";

        public static void SetConnectionstring(string connectionstring)
        {
            Config.CONNECTION_STRING = connectionstring;
        }

        public static void SetDbName(string dbName)
        {
            Config.DB_NAME = dbName;
        }
    }   
}
