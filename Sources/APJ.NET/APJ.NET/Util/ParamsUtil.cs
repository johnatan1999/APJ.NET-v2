using Apj.Net.Dao.Common.Query;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Util
{
    public class ParamsUtil
    {
        public static Criteria [] QueryStringToCriteria(Dictionary<string, object> collection)
        {

            Criteria[] criteria = new Criteria[collection.Count];
            var keys = collection.Keys.ToArray();
            for (int i = 0; i < collection.Count; i++)
            {
                string key = keys[i];
                criteria[i] = Criteria.Eq(key, collection[key]);
            }
            return criteria;
        }

        public static string QueryStringToWhereClause(Dictionary<string, object> collection)
        {

            Criteria[] criteria = new Criteria[collection.Count];
            var keys = collection.Keys.ToArray();
            string whereClause = "";
            for (int i = 0; i < collection.Count; i++)
            {
                string key = keys[i];
                criteria[i] = Criteria.Eq(key, collection[key]);
                whereClause += " and " + key + "=" + collection[key];
            }
            return whereClause;
        }
    }
}
