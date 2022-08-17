using Apj.Net.Dao;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Common.Query;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Util;
using APJ.NET.Models.User;
 using APJ.NET.Models;
using System.Collections.Generic;
using System.Data.Common;
using APJ.NET.Core.ApiResponse;
using APJ.NET.Core.Page;
using System.Reflection;

namespace APJ.NET.Services
{
    public class MenuService
    {
        public void AddMenu(Menu model)
        {
            model.Save();
        }

        public void UpdateMenu(Menu model)
        {
            model.Update();
        }

        public IList<Menu> GetAllMenu()
        {
            IApjDao dao = ApjDaoFactory.GetInstance();
            return dao.FindAll<Menu>();
        }

        public IList<Menu> GetHierarchicalMenu()
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            try
            {
                connection.Open();
                IList<Menu> menus = SearchQuery.findWhere<Menu>(connection, Criteria.IsNull("IdParent"));
                foreach(Menu menu in menus)
                {
                    menu.Submenus = GetSubmenu(menu.Id, connection);
                }
                return menus;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        private IList<Menu> GetSubmenu(string menuId, DbConnection connection)
        {
            IList<Menu> submenu = SearchQuery.findWhere<Menu>(Criteria.Eq("IdParent", menuId));
            if(submenu.Count > 0)
            {
                foreach(Menu menu in submenu)
                {
                    menu.Submenus = GetSubmenu(menu.Id, connection);
                }
            }
            return submenu;
        }

        public PaginatedList SearchMenu(Dictionary<string, object> args, int page, int limit)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            PaginatedList data = new PaginatedList();
            try
            {
                connection.Open();
                data.Data = SearchQuery.findWherePaginated<Menu>(connection, page, limit, ApjDaoUtil.MakeSearchCriteria(typeof(Menu), args).ToArray());
                data.TotalCount = AggregateQuery.CountWhere<Menu>(connection);
                data.Page = page;
                data.Limit = limit;
                return data;
            } 
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public Menu FindMenuById(string id)
        {
            Menu model = new Menu();
            model.Id = id;
            model.Load();
            return model;
        }

        public Menu DeleteMenu(string id)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            try
            {
                connection.Open();
                Menu model = new Menu();
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

        public bool DeleteMenu(string [] ids)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            try
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                foreach(var id in ids)
                {
                    Menu model = new Menu();
                    model.Id = id;
                    model.Delete(transaction);
                }
                transaction.Commit();
                return true;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return false;
        }

        public ObjectModel GetFormModel()
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            ObjectModel model = new ObjectModel();
            try
            {
                connection.Open();
                PropertyInfo[] properties = ApjDaoUtil.GetProperties(typeof(Users));
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
