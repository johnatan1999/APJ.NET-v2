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
    public class ApjServiceWithState<M> : ApjService<M> where M : StateModel, new()
    {
       
        public M ChangeState(string id, int state)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            try
            {
                connection.Open();
                M model = new M();
                model.Id = id;
                DbTransaction transaction = connection.BeginTransaction();
                model.Load(connection);
                model.State = state;
                model.Update(transaction);
                transaction.Commit();
                return model;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public new M Delete(string id, bool permanently)
        {
            if (!permanently)
            {
                return ChangeState(id, State.DELETED);
            }
            else
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
        }

    }
}
