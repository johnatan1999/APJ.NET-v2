using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Common.Query
{
    public class ApjResult
    {
        private Dictionary<string, object> dictionary;

        public ApjResult()
        {
            dictionary = new Dictionary<string, object>();
        }

        public void Set(string key, object value)
        {
            dictionary.Add(key, value);
        }

        public object Get(string key)
        {
            return dictionary[key];
        }

    }
}
