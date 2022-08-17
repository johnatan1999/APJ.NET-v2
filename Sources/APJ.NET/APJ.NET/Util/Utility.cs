using APJ.NET.Core.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APJ.NET.Util
{
    public class Utility
    {
        public static Dictionary<string, string> GetObjectModel2(Type type)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            PropertyInfo[] properties = type.GetProperties();
            foreach(var p in properties)
            {
                if(!p.GetGetMethod().IsVirtual)
                {
                    var typeName = p.PropertyType.Name;
                    switch(typeName)
                    {
                        case "Int32": typeName = "number";
                            break;
                        case "bool": typeName = "boolean";
                            break;
                        default: break;
                    }
                    res.Add(p.Name, typeName);
                }
            }
            return res;
        }

        public static string FirstLetterToLowerCase(string str)
        {
            if(str.Length > 1)
            {
                var tmp = str[0];
                return tmp.ToString().ToLower() + str.Substring(1);
            } return str;
        }

        public static IList<ObjectModelField> GetObjectModel(Type type)
        {
            ObjectModel model = new ObjectModel();
            if(type != null)
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (var p in properties)
                {
                    if (!p.GetGetMethod().IsVirtual)
                    {
                        var typeName = p.PropertyType.Name;
                        switch (typeName)
                        {
                            case "Int32":
                                typeName = "number";
                                break;
                            case "bool":
                                typeName = "boolean";
                                break;
                            default: break;
                        }
                        model.Add(p.Name, typeName);
                    }
                }
            }
            return model.Fields;
        }

        public static IList<ObjectModelField> GetObjectFormModel(Type type)
        {
            ObjectModel model = new ObjectModel();
            if (type != null)
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (var p in properties)
                {
                    if (!p.GetGetMethod().IsVirtual)
                    {
                        var typeName = p.PropertyType.Name;
                        switch (typeName)
                        {
                            case "Int32":
                                typeName = "number";
                                break;
                            case "bool":
                                typeName = "boolean";
                                break;
                            default: break;
                        }
                        model.Add(p.Name, typeName);
                    }
                }
            }
            return model.Fields;
        }
    }
}
