using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Connection
{
    public class OracleConnection
    {
        public static DbConnection GetConnection()
        {
            return new System.Data.OracleClient.OracleConnection(Config.CONNECTION_STRING);
        }
    }
}
