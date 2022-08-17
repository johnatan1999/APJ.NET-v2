using Apj.Net.Dao.Common;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Connection
{
    public static class ConnectionFactory
    {
        public static DbConnection GetConnection()
        {
            switch (Config.DB_NAME.ToUpper())
            {
                case Constante.ORACLE_DB:
                    return OracleConnection.GetConnection();
                case Constante.SQLSERVER_DB:
                    return SqlServerConnection.GetConnection();
                case Constante.POSTGRES_DB:
                    return PostgresConnection.GetConnection();
                default:
                    return null;
            }
        }
    }
}
