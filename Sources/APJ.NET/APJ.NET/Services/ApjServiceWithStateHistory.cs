using Apj.Net.Dao.Connection;
using APJ.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Services
{
    public class ApjServiceWithStateHistory<M> : ApjServiceWithHistory<M> where M : StateModelHistory, new()
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
        public M ChangeState(string id, int state, string userId)
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
                model.UpdateWithHistorique(userId, transaction);
                transaction.Commit();
                return model;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public M Delete(string id, string userId, bool permanently)
        {
            if (!permanently)
            {
                return ChangeState(id, State.DELETED, userId);
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
                    model.DeleteWithHistorique(userId, transaction);
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
