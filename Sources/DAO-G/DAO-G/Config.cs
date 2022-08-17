using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao
{
    internal static class Config
    {
        public static String CONNECTION_STRING
            = "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=TaskDB;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true";
        public static String DB_NAME = "SQL_SERVER";

        public static void SetConnectionString(String connectionString)
        {
            Config.CONNECTION_STRING = connectionString;
        }

        public static void SetDbName(String dbName)
        {
            Config.DB_NAME = dbName;
        }
    }   
}
