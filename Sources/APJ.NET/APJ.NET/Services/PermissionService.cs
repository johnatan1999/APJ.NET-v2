using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Util;
using APJ.NET.Core.ApiResponse;
using APJ.NET.Core.Page;
using APJ.NET.Models;
using APJ.NET.Models.User;
using APJ.NET.Util;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace APJ.NET.Services
{
    public class PermissionService
    {
        public void AddPermission(Permission model)
        {
            model.Save();
        }

        public void UpdatePermission(Permission model)
        {
            model.Update();
        }

        public IList<Permission> GetAllPermission()
        {
            IApjDao dao = ApjDaoFactory.GetInstance();
            return dao.FindAll<Permission>();
        }

        public PaginatedList SearchPermission(Dictionary<string, object> args, int page, int limit)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            PaginatedList data = new PaginatedList();
            try
            {
                connection.Open();
                data.Data = SearchQuery.findWherePaginated<Permission>(connection, page, limit, ApjDaoUtil.MakeSearchCriteria(typeof(Permission), args).ToArray());
                data.TotalCount = AggregateQuery.CountWhere<Permission>(connection);
                data.Page = page;
                data.Limit = limit;
                return data;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public Permission FindPermissionById(string id)
        {
            Permission model = new Permission();
            model.Id = id;
            model.Load();
            return model;
        }

        public Permission DeletePermission(string id)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            try
            {
                connection.Open();
                Permission model = new Permission();
                model.Id = id;
                DbTransaction transaction = connection.BeginTransaction();
                model.Load(connection);
                model.Delete(transaction);
                transaction.Commit();
                return model;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        
        public ObjectModel GetFormModel()
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            ObjectModel model = new ObjectModel();
            try
            {
                connection.Open();
                PropertyInfo[] properties = ApjDaoUtil.GetProperties(typeof(Permission));
                foreach (var p in properties)
                {
                    string table = AnnotationUtil.GetReferencedTable(p);
                    var typeName = p.PropertyType.Name;
                    ObjectModelField field = new ObjectModelField();
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
                    field.Type = typeName;
                    if (table != null)
                    {
                        field.Type = "select";
                        List<ApjResult> res = Query.Execute("select * from " + table, connection) as List<ApjResult>;
                        object[] data = new object[res.Count];
                        for(int i=0; i<res.Count; i++)
                        {
                            data[i] = res[i].GetData();
                        }
                        field.Data = data;
                        field.DataFieldLabel = "Description";
                    }
                    field.Name = field.Label = p.Name;
                    model.Add(field);
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
