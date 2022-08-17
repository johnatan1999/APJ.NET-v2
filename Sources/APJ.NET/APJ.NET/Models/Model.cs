using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using APJ.NET.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models
{
    public class Model : BaseModel
    {
        public void SaveWithHistorique(string userId)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                this.SaveWithHistorique(userId, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public void SaveWithHistorique(string userId, DbTransaction transaction)
        {
            try
            {
                this.Save(transaction);
                Historique h = new Historique();
                h.Object = this.GetTableName();
                h.IdUser = userId;
                h.RefObject = this.Id;
                h.Action = "Insertion par " + userId;
                h.DateAction = DateTime.Now;
                h.TimeAction = DateTime.Now.TimeOfDay.ToString();
                h.Save(transaction);
            }
            catch(Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
        }

        public void UpdateWithHistorique(string userId)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                this.UpdateWithHistorique(userId, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public void UpdateWithHistorique(string userId, DbTransaction transaction)
        {
            try
            {
                this.Update(transaction);
                Historique h = new Historique();
                h.Object = this.GetTableName();
                h.IdUser = userId;
                h.RefObject = this.Id;
                h.Action = "Modification par " + userId;
                h.DateAction = DateTime.Now;
                h.TimeAction = DateTime.Now.TimeOfDay.ToString();
                h.Save(transaction);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
        }

        public void DeleteWithHistorique(string userId)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                this.DeleteWithHistorique(userId, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        public void DeleteWithHistorique(string userId, DbTransaction transaction)
        {
            try
            {
                this.Delete(transaction);
                Historique h = new Historique();
                h.Object = this.GetTableName();
                h.IdUser = userId;
                h.RefObject = this.Id;
                h.Action = "Supression par " + userId;
                h.DateAction = DateTime.Now;
                h.TimeAction = DateTime.Now.TimeOfDay.ToString();
                h.Save(transaction);
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
        }

    }
}
