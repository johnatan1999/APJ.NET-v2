using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Util;
using APJ.NET.Core.ApiResponse;
using APJ.NET.Core.Page;
using APJ.NET.Models;
using APJ.NET.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace APJ.NET.Services
{
    public class RoleService : ApjServiceWithHistory<Role>
    {
       
        public new ObjectModel GetFormModel()
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            ObjectModel model = new ObjectModel();
            try
            {
                connection.Open();
                PropertyInfo[] properties = ApjDaoUtil.GetProperties(typeof(Role));
                foreach(var p in properties)
                {
                    string table = AnnotationUtil.GetReferencedTable(p);
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
                    object[] data = null;
                    if(table != null)
                    {
                        List<ApjResult> res = Query.Execute("select * from "+table, connection) as List<ApjResult>;
                        data = res.ToArray();
                    }
                    model.Add(p.Name, typeName, null, data);
                }
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return model;
        }

    }
}
