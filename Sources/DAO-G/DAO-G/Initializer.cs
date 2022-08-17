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
            Config.CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=TaskDB;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true";
            Config.DB_NAME = Constante.SQLSERVER_DB;
        }
    }
}
