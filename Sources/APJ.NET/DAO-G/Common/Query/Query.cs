using Apj.Net.Dao.Connection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public static class Query
    {

        public static IList<ApjResult> Execute(string sql)
        {
            DbConnection connection = null;
            try {
                connection = ConnectionFactory.GetConnection();
                connection.Open();
                return Execute(sql, connection);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public static IList<ApjResult> Execute(string sql, DbConnection connection)
        {
            IList<ApjResult> res = new List<ApjResult>();
            Console.WriteLine("Query $> " + sql);
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ApjResult row = new ApjResult();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string colName = rdr.GetName(i);
                        row.Set(colName, rdr[colName]);
                    }
                    res.Add(row);
                }
                return res;
            } 
            finally
            {
                if (rdr != null) rdr.Close();
            }
        }
    }
}
