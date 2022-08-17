using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Apj.Net.Dao.Util
{
    internal static class Utility
    {

        public static string ReplaceMultiplceSpacingByOne(string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }

        public static PropertyInfo [] GetProperties(Type type)
        {
            List<PropertyInfo> tmp = new List<PropertyInfo>();
            foreach (var p in tmp)
            {
                if (!p.GetGetMethod().IsVirtual)
                {
                    tmp.Add(p);
                }
            }
            return tmp.ToArray();
        }

    }
}
