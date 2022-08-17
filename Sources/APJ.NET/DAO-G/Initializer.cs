using Apj.Net.Dao.Common;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao
{
    public class Initializer
    {
        public static void Init()
        {
            Config.CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APJ.NET;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Config.DB_NAME = Constante.SQLSERVER_DB;
        }

        public static void SetConnectionString(string str)
        {
            Config.CONNECTION_STRING = str;
        }

        public static void SetDbName(string str)
        {
            Config.DB_NAME = str;
        }
    }
}
