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
        public int GetInt(string key)
        {
            return int.Parse(dictionary[key].ToString());
        }

        public object Get(int index)
        {
            string[] keys = new string[dictionary.Count];
            dictionary.Keys.CopyTo(keys, 0);
            return dictionary[keys[index]];
        }

        public int GetInt(int index)
        {
            string[] keys = new string[dictionary.Count];
            dictionary.Keys.CopyTo(keys, 0);
            return int.Parse(dictionary[keys[index]].ToString());
        }

        public Dictionary<string, object> GetData()
        {
            return dictionary;
        }

    }
}
