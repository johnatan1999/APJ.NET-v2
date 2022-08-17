using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public class AggregateQuery
    {
        public static int CountWhere<M>(params Criteria[] criteria) where M : BaseModel
        {
            DbConnection cnn = ConnectionFactory.GetConnection();
            try
            {
                cnn.Open();
                return CountWhere<M>(cnn, criteria);
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
        }

        public static int CountWhere<M>(DbConnection connection, params Criteria[] criteria) where M : BaseModel
        {
            string sql = "SELECT count(*) FROM " + ApjDaoUtil.GetTableNameByType(typeof(M));
            bool criteriaEmpty = criteria == null;
            if (!criteriaEmpty)
            {
                sql += " WHERE 1=1 " + Criteria.JoinCriteria(criteria);
            }
            Console.WriteLine("AggregateQuiery.CountWhere " + sql);
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                if (!criteriaEmpty)
                {
                    foreach (Criteria c in criteria)
                    {
                        DbParameter parameter = cmd.CreateParameter();
                        parameter.ParameterName = "@" + c.SqlParameter.Split("@")[0];
                        parameter.Value = c.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }
                rdr = cmd.ExecuteReader();
                if (rdr == null) return 0;
                if(rdr.Read())
                    return int.Parse(rdr[0].ToString());
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
            return 0;
        }


        public static int CountWhere<M>(string whereClause) where M : BaseModel
        {
            DbConnection cnn = ConnectionFactory.GetConnection();
            try
            {
                cnn.Open();
                return CountWhere<M>(cnn, whereClause);
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
        }

        public static int CountWhere<M>(DbConnection connection, string whereClause) where M : BaseModel
        {
            whereClause = whereClause.TrimStart();
            if (!whereClause.ToUpper().StartsWith("AND "))
            {
                whereClause = "AND " + whereClause;
            }
            string sql = "SELECT count(*) FROM " + ApjDaoUtil.GetTableNameByType(typeof(M))
                + " WHERE 1=1 " + whereClause;
            Console.WriteLine("AggregateQuiery.CountWhere " + sql);
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                rdr = cmd.ExecuteReader();
                if (rdr == null) return 0;
                if(rdr.Read())
                    return int.Parse(rdr[1].ToString());
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
            return 0;
        }
    }
}
