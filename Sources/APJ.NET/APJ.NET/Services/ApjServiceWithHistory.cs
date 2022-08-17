using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using APJ.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Services
{
    public class ApjServiceWithHistory<M> : ApjService<M> where M : BaseModelHistory, new()
    {
        public void Add(M model, string userId)
        {
            model.SaveWithHistorique(userId);
        }

        public void Update(M model, string userId)
        {
            model.UpdateWithHistorique(userId);
        }

        public M Delete(string id, string userId)
        {
            DbConnection connection = ConnectionFactory.GetConnection(); ;
            try
            {
                connection.Open();
                M model = new M();
                model.Id = id;
                model.Load(connection);
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
