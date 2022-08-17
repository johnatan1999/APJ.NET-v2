using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Connection
{
    class PostgresConnection
    {
        public static DbConnection GetConnection()
        {
            return new NpgsqlConnection(Config.CONNECTION_STRING);
        }
    }
}
