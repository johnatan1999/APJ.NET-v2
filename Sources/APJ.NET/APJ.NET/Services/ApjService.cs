using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;
using APJ.NET.Core.ApiResponse;
using APJ.NET.Core.Page;
using APJ.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APJ.NET.Services
{
    public class ApjService<M> where M : BaseModel, new()
    {
        public void Add(M model) 
        {
            model.Save();
        }

        public void Update(M model)
        {
            model.Update();
        }

        public IList<M> GetAll()
        {
            IApjDao dao = ApjDaoFactory.GetInstance();
            return dao.FindAll<M>();
        }

        public PaginatedList Search(Dictionary<string, object> args, int page, int limit)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            PaginatedList data = new PaginatedList();
            try
            {
                connection.Open();
                data.Data = SearchQuery.findWherePaginated<M>(connection, page, limit, ApjDaoUtil.MakeSearchCriteria(typeof(M), args).ToArray());
                data.TotalCount = AggregateQuery.CountWhere<M>(connection);
                data.Page = page;
                data.Limit = limit;
                return data;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public M FindById(string id)
        {
            M model = new M();
            model.Id = id;
            model.Load();
            return model;
        }

        public M Delete(string id)
        {
            DbConnection connection = ConnectionFactory.GetConnection(); ;
            try
            {
                connection.Open();
                M model = new M();
                model.Id = id;
                DbTransaction transaction = connection.BeginTransaction();
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
                PropertyInfo[] properties = ApjDaoUtil.GetProperties(typeof(M));
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
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (i == 0)
                                field.Value = res[i].Get("Id");
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
