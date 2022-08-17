using Apj.Net.Dao.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Text;

namespace Apj.Net.Dao.Common
{
    internal static class ApjCommandFactory
    {
        public static DbCommand CreateCommand(String query)
        {
            switch (Config.DB_NAME.ToUpper())
            {
                case Constante.ORACLE_DB:
                    return new OracleCommand(query);
                case Constante.SQLSERVER_DB:
                    return new SqlCommand(query);
                case Constante.POSTGRES_DB:
                    return new NpgsqlCommand(query);
                default:
                    return null;
            }
        }

        public static DbCommand CreateCommand(String query, DbConnection? connection)
        {
            switch (Config.DB_NAME.ToUpper())
            {
                case Constante.ORACLE_DB:
                    return new OracleCommand(query, (System.Data.OracleClient.OracleConnection)connection);
                case Constante.SQLSERVER_DB:
                    return new SqlCommand(query, (SqlConnection)connection);
                case Constante.POSTGRES_DB:
                    return new NpgsqlCommand(query, (NpgsqlConnection)connection);
                default:
                    return null;
            }
        }

        public static DbCommand CreateCommand(String query, DbConnection? connection, DbTransaction? transaction)
        {
            switch (Config.DB_NAME.ToUpper())
            {
                case Constante.ORACLE_DB:
                    return new OracleCommand(query, (System.Data.OracleClient.OracleConnection)connection, (OracleTransaction)transaction);
                case Constante.SQLSERVER_DB:
                    return new SqlCommand(query, (SqlConnection)connection, (SqlTransaction)transaction);
                case Constante.POSTGRES_DB:
                    return new NpgsqlCommand(query, (NpgsqlConnection)connection, (NpgsqlTransaction)transaction);
                default:
                    return null;
            }
        }
    }
}
