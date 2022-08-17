using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Apj.Net.Dao.Connection
{
    public class SqlServerConnection
    {
        public static DbConnection GetConnection()
        {
            return new SqlConnection(Config.CONNECTION_STRING);
        }
    }
}
