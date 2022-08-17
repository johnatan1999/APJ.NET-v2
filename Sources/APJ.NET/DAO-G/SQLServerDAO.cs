using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Apj.Net.Dao.Common;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using Apj.Net.Dao.Util;

namespace Apj.Net.Dao
{
    internal class SQLServerDAO : IApjDao
    {
        #region Add
        public void Add<M>(M model) where M : BaseModel
        {
            using DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    this.Add(model, transaction);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                //if (transaction != null) transaction.Rollback();
                Console.WriteLine(ex.Message+"\n"+ex.StackTrace);
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }
        public void Add<M>(M model, DbTransaction transaction) where M : BaseModel
        {
            string query = QueryInitializer.CreateInsertQuery(model);
            try
            {
                ISequenceGenerator seq = ApjSequenceGeneratorFactory.GetSequenceGenerator();
                int nextSeq = seq.GetNextSequence(model, transaction);
                model.Id = ApjDaoUtil.BuildPK(model, nextSeq);
                DbCommand cmd = ApjCommandFactory.CreateCommand(query, transaction.Connection, transaction);
                PropertyInfo[] properties = ApjDaoUtil.GetProperties(model.GetType());
                foreach (PropertyInfo p in properties)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = "@" + p.Name;
                    var value = p.GetValue(model);
                    if (value == null)
                        value = DBNull.Value;
                    dbParameter.Value = value;
                    Console.WriteLine(p.Name +"="+ p.GetValue(model)+" t="+p.PropertyType);
                    cmd.Parameters.Add(dbParameter);
                }
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                if(transaction != null)
                    transaction.Rollback();
                Console.WriteLine(ex.Message+"\n"+ex.StackTrace);
                throw ex;
            }
        }
        #endregion

        #region Count
        public int Count()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public void Delete<M>(M model) where M : BaseModel
        {
            using DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    this.Delete(model, transaction);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                //if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }
        public void Delete<M>(M model, DbTransaction transaction) where M : BaseModel
        {
            string query = QueryInitializer.CreateDeleteQuery(model);
            DbCommand cmd = ApjCommandFactory.CreateCommand(query, transaction.Connection, transaction);
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.ParameterName = "@" + model.GetAttributIDName();
            dbParameter.Value = model.Id;
            cmd.Parameters.Add(dbParameter);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region FindById
        public M FindById<M>(M model) where M : BaseModel
        {
            using (DbConnection connection = ConnectionFactory.GetConnection())
            {
                DbTransaction transaction = null;
                try
                {
                    if (connection != null)
                    {
                        connection.Open();
                        return this.FindById<M>(model, connection);
                    }
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            return null;
        }
        
        public M FindById<M>(M model, DbConnection connection) where M : BaseModel
        {
            DbDataReader rdr = null;
            try
            {
                var query = "select * from " + model.GetTableName() 
                    + " where " + model.GetAttributIDName() 
                    + " = @" + model.GetAttributIDName();
                DbCommand cmd = ApjCommandFactory.CreateCommand(query, connection, null);
                DbParameter parameter = cmd.CreateParameter();
                parameter.ParameterName = "@" + model.GetAttributIDName();
                parameter.Value = model.Id;
                parameter.DbType = ApjFieldUtil.GetPropertyDbType(model.Id.GetType());
                cmd.Parameters.Add(parameter);
                if (cmd != null)
                {
                    rdr = cmd.ExecuteReader();
                    IList<M> data = ApjDaoUtil.Load<M>(model.GetType(), rdr);
                    cmd.Dispose();
                    if (data.Count > 0)
                    {
                        PropertyInfo[] properties = ApjDaoUtil.GetProperties(model.GetType());
                        foreach (PropertyInfo p in properties)
                        {
                            p.SetValue(model, p.GetValue(data[0]));
                        }
                        return model;
                    }
                }
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
            return null;
        }
        #endregion

        #region GetAll
        public IList<M> FindAll<M>() where M : BaseModel
        {
            DbConnection cnn = ConnectionFactory.GetConnection();
            DbTransaction transaction;
            try
            {
                cnn.Open();
                return this.FindAll<M>(cnn);    
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
        }

        public IList<M> FindAll<M>(DbConnection connection) where M : BaseModel
        {
            DbDataReader rdr = null;
            IList<M> list = new List<M>();
            try
            {
                BaseModel model = (BaseModel)Activator.CreateInstance(typeof(M));
                var query = "select * from " + model.GetTableName();
                DbCommand cmd = ApjCommandFactory.CreateCommand(query, connection, null);
                if (cmd != null)
                {
                    rdr = cmd.ExecuteReader();
                    list = ApjDaoUtil.Load<M>(rdr);
                    cmd.Dispose();
                }
                return list;
            }
            finally
            {
                if (rdr != null) rdr.Close();
            }
        }
        #endregion

        #region Update
        public void Update<M>(M model) where M : BaseModel
        {
            using DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    this.Update(model, transaction);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                //if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Update<M>(M model, DbTransaction transaction) where M : BaseModel
        {
            var query = QueryInitializer.CreateUpdateQuery(model);
            DbCommand cmd = ApjCommandFactory.CreateCommand(query, transaction.Connection, transaction);
            PropertyInfo[] properties = ApjDaoUtil.GetProperties(model.GetType());
            foreach (PropertyInfo p in properties)
            {
                DbParameter dbParameter = cmd.CreateParameter();
                dbParameter.ParameterName = "@" + p.Name;
                var value = p.GetValue(model);
                if (value == null)
                    value = DBNull.Value;
                dbParameter.Value = value;
                cmd.Parameters.Add(dbParameter);
            }
            cmd.ExecuteNonQuery();
        }
        #endregion
    }


}
