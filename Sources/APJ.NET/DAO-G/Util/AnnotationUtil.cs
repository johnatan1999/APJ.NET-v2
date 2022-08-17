using Apj.Net.Dao.Annotations;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Util
{
    public class AnnotationUtil
    {

        public static string GetReferencedTable(PropertyInfo property)
        {
            Attribute a = property.GetCustomAttribute(typeof(ForeignKey));
            if(a != null)
            {
                ForeignKey fk = (ForeignKey)a;
                return fk.tableName;
            }
            return null;
        }

        public static List<BaseModel> GetFKReference(BaseModel model)
        {
            Type type = model.GetType();
            List<BaseModel> res = new List<BaseModel>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                Attribute a = p.GetCustomAttribute(typeof(ForeignKey));
                if(a is ForeignKey)
                {
                    ForeignKey fk = (ForeignKey)a;
                    var fkReference = type.GetProperty(fk.name);
                    if (fkReference != null)
                    {
                        if(fkReference.PropertyType == typeof(BaseModel))
                        {
                            BaseModel obj = (BaseModel) Activator.CreateInstance(fkReference.PropertyType);
                            obj.Id = p.GetValue(model).ToString();
                            fkReference.SetValue(model, obj);
                            res.Add(obj);
                        }
                    }
                }
            }
            return res;
        }
    }
}
