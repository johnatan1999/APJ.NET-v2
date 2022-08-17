using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public class QueryBuilder<M> where M : BaseModel
    {
        private const string QueryString = " Select";

        string ColumnToSelect = " * ";

        string TableName = "";

        private string Joiner = "";

        string CriteriaString = "";

        string _OrderBy = "";

        string Order = "";

        string _GroupBy = "";

        private List<Criteria> criteria;

        public QueryBuilder() 
        {
            TableName = ApjDaoUtil.GetTableNameByType(typeof(M));
            criteria = new List<Criteria>();
        } 

        public void resetQuery()
        {
            ColumnToSelect = " * ";
            CriteriaString = " ";
            _GroupBy = " ";
            Joiner = " ";
            Order = " ";
            _OrderBy = " ";
        }

        public QueryBuilder<M> Select(params string [] columns)
        {
            foreach (string s in columns) ColumnToSelect = " " + s + ",";
            if (!"".Equals(ColumnToSelect)) 
                ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> Count(string [] columns)
        {
            foreach (string s in columns) ColumnToSelect += " count(" + s + "),";
            if (!"".Equals(ColumnToSelect)) 
                ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> Distinct(params string [] columns)
        {
            foreach (string s in columns) ColumnToSelect += " distinct(" + s + "),";
            if (!"".Equals(ColumnToSelect)) ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> Sum(params string [] columns)
        {
            foreach (string s in columns) ColumnToSelect += " sum(" + s + "),";
            if (!"".Equals(ColumnToSelect)) ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> Min(params string [] columns)
        {
            foreach (string s in columns) ColumnToSelect += " min(" + s + "),";
            if (!"".Equals(ColumnToSelect)) ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> Max(params string [] columns)
        {
            foreach (string s in columns) ColumnToSelect += " max(" + s + "),";
            if (!"".Equals(ColumnToSelect)) ColumnToSelect = ColumnToSelect.Substring(0, ColumnToSelect.Length - 1);
            return this;
        }

        public QueryBuilder<M> OrderBy(string column, params string [] columns)
        {
            string tmp = column;
            foreach (string c in columns) tmp += ", " + c;
            _OrderBy += tmp;
            return this;
        }

        public QueryBuilder<M> GroupBy(string column, params string [] columns)
        {
            string tmp = column;
            foreach (string c in columns) tmp += ", " + c;
            _GroupBy += tmp;
            return this;
        }

        public QueryBuilder<M> Asc()
        {
            Order = " ASC";
            return this;
        }
        
        public QueryBuilder<M> Desc()
        {
            Order = " DESC";
            return this;
        }

        public QueryBuilder<M> WhereEq(string column, object value)
        {
            criteria.Add(Criteria.Eq(column, value));
            return this;
        }

        public QueryBuilder<M> WhereNotEq(string column, object value)
        {
            criteria.Add(Criteria.NotEq(column, value));
            return this;
        }
        
        public QueryBuilder<M> WhereSupEq(string column, object value)
        {
            criteria.Add(Criteria.SupEq(column, value));
            return this;
        }
        
        public QueryBuilder<M> WhereSup(string column, object value)
        {
            criteria.Add(Criteria.Sup(column, value));
            return this;
        }

        public QueryBuilder<M> WhereInfEq(string column, object value)
        {
            criteria.Add(Criteria.InfEq(column, value));
            return this;
        }

        public QueryBuilder<M> WhereInf(string column, object value)
        {
            criteria.Add(Criteria.Inf(column, value));
            return this;
        }

        public QueryBuilder<M> WhereStartsWith(string column, object value)
        {
            criteria.Add(Criteria.StartsWith(column, value));
            return this;
        }
        
        public QueryBuilder<M> WhereEndsWith(string column, object value)
        {
            criteria.Add(Criteria.EndsWith(column, value));
            return this;
        }

        public QueryBuilder<M> WhereContains(string column, object value)
        {
            criteria.Add(Criteria.Contains(column, value));
            return this;
        }

        public IList<ApjResult> Execute()
        {
            DbConnection connection = null;
            try
            {
                connection = ConnectionFactory.GetConnection();
                connection.Open();
                return Execute(GetQueryString(), connection);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public IList<ApjResult> Execute(string sql, DbConnection connection)
        {
            IList<ApjResult> res = new List<ApjResult>();
            DbCommand cmd = ApjCommandFactory.CreateCommand(sql, connection);
            DbDataReader rdr = null;
            try
            {
                foreach(Criteria c in criteria)
                {
                    DbParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = Criteria.RemoveOrAnd(c.Column);
                    parameter.Value = c.Value;
                    cmd.Parameters.Add(parameter);
                }
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ApjResult row = new ApjResult();
                    for(int i=0; i<rdr.FieldCount; i++)
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

        public string GetQueryString()
        {
            CriteriaString = Criteria.JoinCriteria(criteria.ToArray());
            string _groupBy = _GroupBy.Length > 0 ? " GROUP BY " + _GroupBy : "";
            string _orderBy = _OrderBy.Length > 0 ? " ORDER BY " + _OrderBy : "";
            return QueryString + ColumnToSelect + " FROM " + TableName
                + " WHERE 1=1 " + CriteriaString + _groupBy + _orderBy + Order;
        }

        public override string ToString()
        {
            return GetQueryString();
        }
    }
}
