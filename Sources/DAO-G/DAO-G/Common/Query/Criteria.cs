using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public class Criteria
    {
        private string _column;

        public string Column
        {
            get { return _column; }
            set { 
                _column = Utility.ReplaceMultiplceSpacingByOne(value).TrimStart();
            }
        }


        public object Value { get; set; }

        private string _sqlParameter;

        public string SqlParameter
        {
            get { return _sqlParameter; }
            set {
                Operator = Operator.Replace(Column, value);
                _sqlParameter = value; 
            }
        }


        private string Operator { get; set; }

        Criteria(string column, object value)
        {
            this.Column = column;
            this.Value = value;
        }

        private Criteria(string column, string _operator, object value)
        {
            this.Column = column;
            this.Value = value;
            this.Operator = _operator;
            this.SqlParameter = column;
        }

        public static Criteria Eq(string column, object value)
        {
            return new Criteria(column, " = " + GetQueryParameterKey(column), value);
        }

        public static Criteria NotEq(string column, object value)
        {
            return new Criteria(column, " != " + GetQueryParameterKey(column), value);
        }

        public static Criteria Sup(string column, object value)
        {
            return new Criteria(column, " > " + GetQueryParameterKey(column), value);
        }

        public static Criteria Inf(string column, object value)
        {
            return new Criteria(column, " < " + GetQueryParameterKey(column), value);
        }

        public static Criteria SupEq(string column, object value)
        {
            return new Criteria(column, " >= " + GetQueryParameterKey(column), value);
        }

        public static Criteria InfEq(string column, object value)
        {
            return new Criteria(column, " <= " + GetQueryParameterKey(column), value);
        }

        //public static Criteria Between(string column, object value, object value2)
        //{
        //    return new Criteria(column, " > " + GetQueryParameterKey(column) 
        //             +" and " + column + " < " + GetQueryParameterKey(column), value);
        //}

        //public static Criteria BetweenEq(string column, object value, object value2)
        //{
        //    return new Criteria(string.Concat(column, " >= " + GetQueryParameterKey(column) +" and ", column, " <= " + GetQueryParameterKey(column)), value);
        //}

        public static Criteria Contains(string column, object value)
        {
            value = "%" + value + '%';
            return new Criteria(column, " like " + GetQueryParameterKey(column), value);
        }

        public static Criteria StartsWith(string column, object value)
        {
            value = value + "%";
            return new Criteria(column, " like " + GetQueryParameterKey(column), value);
        }

        public static Criteria EndsWith(string column, object value)
        {
            value = "%" + value;
            return new Criteria(column, " like " + GetQueryParameterKey(column), value);
        }

        internal static string JoinCriteria(Criteria [] criteria)
        {
            string tmp = "";
            foreach (Criteria crit in criteria)
            {
                if (!crit.Column.ToUpper().StartsWith("AND ")
                    && !crit.Column.ToUpper().StartsWith("OR "))
                    tmp += " AND " + crit;
                else tmp += " " + crit;
            }
            return tmp;
        }

        public override string ToString()
        {
            return Column + Operator;
        }

        private static string GetQueryParameterKey(string str)
        {
            string s = Utility.ReplaceMultiplceSpacingByOne(str);
            switch(Config.DB_NAME)
            {
                case Constante.SQLSERVER_DB:
                    s = RemoveOrAnd(s);
                    return "@" + s.TrimStart();
                default: return "?";
            }
        } 

        internal static string RemoveOrAnd(string column)
        {
            string s = column;
            if (s.ToUpper().StartsWith("AND "))
                s = s.Substring(4);
            else if (s.ToUpper().StartsWith("OR "))
            {
                s = s.Substring(3);
            }
            return s;
        }
    }
}
