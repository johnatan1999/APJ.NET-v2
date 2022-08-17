using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public static class SearchQuery
    {

        public static IList<M> findWhere<M>(params Criteria [] criteria) where M : BaseModel
        { 
            DbConnection cnn = ConnectionFactory.GetConnection();
            try
            {
                cnn.Open();
                return findWhere<M>(cnn, criteria);    
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
        }    

        public static IList<M> findWhere<M>(DbConnection connection, params Criteria [] criteria) where M : BaseModel
        {
            string sql = "SELECT * FROM " + ApjDaoUtil.GetTableNameByType(typeof(M));
            bool criteriaEmpty = criteria == null;
            if(!criteriaEmpty) {
                sql += " WHERE 1=1 " + Criteria.JoinCriteria(criteria);
            }
            Console.WriteLine("QueryBuilder.findWhere "+sql);
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                if (!criteriaEmpty)
                {
                    foreach (Criteria c in criteria)
                    {
                        DbParameter parameter = cmd.CreateParameter();
                        parameter.ParameterName = "@"+c.SqlParameter.Split("@")[0];
                        parameter.Value = c.Value;
                        cmd.Parameters.Add(parameter);
                        Console.WriteLine("== @"+c.SqlParameter + "="+c.Value+" /"+c.Value.GetType());
                    }
                    rdr = cmd.ExecuteReader();
                }
                if (rdr == null) return new List<M>();
                return ApjDaoUtil.Load<M>(rdr);
            } 
            finally
            {
                if (rdr != null) rdr.Close();
            }
        }


        public static IList<M> findWhere<M>(string whereClause) where M : BaseModel
        {
            DbConnection cnn = ConnectionFactory.GetConnection();
            try
            {
                cnn.Open();
                return findWhere<M>(cnn, whereClause);
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
        }

        public static IList<M> findWhere<M>(DbConnection connection, string whereClause) where M : BaseModel
        {
            whereClause = whereClause.TrimStart();
            if(!whereClause.ToUpper().StartsWith("AND "))
            {
                whereClause = "AND " + whereClause;
            }
            string sql = "SELECT * FROM " + ApjDaoUtil.GetTableNameByType(typeof(M)) 
                + " WHERE 1=1 " + whereClause;
            Console.WriteLine("QueryBuilder.findWhere " + sql);
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                rdr = cmd.ExecuteReader();
                if (rdr == null) return new List<M>();
                return ApjDaoUtil.Load<M>(rdr);
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
        }
    }
}
