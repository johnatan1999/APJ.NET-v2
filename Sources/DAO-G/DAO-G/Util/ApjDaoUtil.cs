using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Util
{
    public class ApjDaoUtil
    {
        internal static IList<M> Load<M>(DbDataReader rdr) where M : BaseModel
        {
            Type c = typeof(M);
            //FieldInfo [] fields = c.GetFields();
            IList<M> data = new List<M>();
            while(rdr.Read())
            {
                var obj = Activator.CreateInstance(c);
                for(int i=0; i<rdr.FieldCount; i++)
                {
                    string columnName = rdr.GetName(i);
                    PropertyInfo p = c.GetProperty(columnName);
                    if(p != null)
                    {
                        p.SetValue(obj, rdr[columnName]);
                        //MethodInfo[] accessors = p.GetAccessors();
                        //if (accessors.Length > 1)
                        //{
                        //    foreach (MethodInfo method in accessors)
                        //    {
                        //        if (method.ReturnType.Equals(typeof(void)))
                        //        {
                        //            method.Invoke(obj, new object[] { rdr[columnName] });
                        //        }
                        //    }
                        //}
                    }
                }
                data.Add((M)obj);
            }
            return data;
        }

        internal static IList<M> Load<M>(Type c, DbDataReader rdr) where M : BaseModel
        {
            IList<M> data = new List<M>();
            while (rdr.Read())
            {
                var obj = Activator.CreateInstance(c);
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string columnName = rdr.GetName(i);
                    PropertyInfo p = c.GetProperty(columnName);
                    if (p != null)
                    {
                        p.SetValue(obj, rdr[columnName]);
                        //MethodInfo[] accessors = p.GetAccessors();
                        //if (accessors.Length > 1)
                        //{
                        //    foreach (MethodInfo method in accessors)
                        //    {
                        //        if (method.ReturnType.Equals(typeof(void)))
                        //        {
                        //            method.Invoke(obj, new object[] { rdr[columnName] });
                        //        }
                        //    }
                        //}
                    }
                }
                data.Add((M)obj);
            }
            return data;
        }

        /// <summary>
        /// Build the model PK format and return the created PK
        /// </summary>
        /// <param name="model"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string BuildPK(BaseModel model, int no)
        {
            string pk = model.GetIndicePK();
            int zeroCount = model.GetPkLength() - (pk.Length + (no+"").Length);
            for (int i = 0; i < zeroCount; i++)
                pk += "0";
            model.Id = pk + no;
            return model.Id;
        }

        public static string GetTableNameByType(Type type)
        {
            object o = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("GetTableName");
            return method.Invoke(o, new object[] { }).ToString();
        }

        public static List<Criteria> MakeSearchCriteria(Type type, Dictionary<string, object> parameters)
        {
            List<Criteria> criteria = new List<Criteria>();
            var keys = parameters.Keys;
            foreach(var key in keys)
            {
                var intervalKey = key.Split("-")[0];
                if(key.EndsWith("-1") && GetPropertyByName(type, intervalKey) != null)
                {
                    var c = Criteria.SupEq(intervalKey, parameters[key]);
                    c.SqlParameter = key.Replace("-", "_");
                    criteria.Add(c);
                }
                else if (key.EndsWith("-2") && GetPropertyByName(type, intervalKey) != null)
                {
                    var c = Criteria.InfEq(intervalKey, parameters[key]);
                    c.SqlParameter = key.Replace("-", "_");
                    criteria.Add(c);
                }
                else if (GetPropertyByName(type, key) != null)
                {
                    criteria.Add(Criteria.Contains(key, parameters[key]));
                } 
            }
            return criteria;
        }

        /// <summary>
        /// Get PropertyInfo by name ignoring case
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyByName(Type type, string name)
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach(var p in properties)
            {
                if (p.Name.ToLower().Equals(name.ToLower()))
                    return p;
            }
            return null;
        }
    }

}
