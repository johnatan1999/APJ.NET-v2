using Apj.Net.Dao.Common;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Util
{
    internal class QueryInitializer
    {
        public static string CreateInsertQuery<M>(M model) where M : BaseModel
        {
            string queryPrameterId = QueryParameter.GetQueryParameterID();
            string sql = "INSERT INTO " + model.GetTableName();
            PropertyInfo[] properties = ApjDaoUtil.GetProperties(model.GetType());
            string columns = "";
            string values = "";
            for (int i = 0; i < properties.Length; i++)
            {
                columns += properties[i].Name + ", ";
                //if (Config.DB_NAME != Constante.SQLSERVER_DB)
                //    values += "?, ";
                //else 
                    values += queryPrameterId + properties[i].Name + ", ";
            }
            values = values.Substring(0, values.Length - 2);
            columns = columns.Substring(0, columns.Length - 2);
            sql += "(" + columns + ") VALUES(" + values + ")";
            return sql;
        }

        public static string CreateUpdateQuery<M>(M model) where M : BaseModel
        {
            string sql = "UPDATE " + model.GetTableName() + " SET ";
            string queryPrameterId = QueryParameter.GetQueryParameterID();
            PropertyInfo[] properties = ApjDaoUtil.GetProperties(model.GetType());
            foreach (PropertyInfo p in properties)
            {
                if(p.Name != model.GetAttributIDName())
                {
                    if (Config.DB_NAME != Constante.SQLSERVER_DB)
                        sql += p.Name + " = ? ,";
                    else sql += p.Name + " = "+ queryPrameterId + p.Name + ", ";
                }
            }
            sql = sql.Substring(0, sql.Length - 2);
            sql += " WHERE "+ model.GetAttributIDName() +" = "+queryPrameterId+"Id";
            return sql;
        }

        public static string CreateDeleteQuery<M>(M model) where M : BaseModel
        {
            string queryPrameterId = QueryParameter.GetQueryParameterID();
            string sql = "DELETE FROM " + model.GetTableName() + " WHERE "+ model.GetAttributIDName() +" = "+queryPrameterId+"Id";
            return sql;
        }
 
    }
}
