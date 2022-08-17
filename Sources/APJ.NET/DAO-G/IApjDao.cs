using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace Apj.Net.Dao
{
    public interface IApjDao
    {
        IList<M> FindAll<M>() where M : BaseModel;

        IList<M> FindAll<M>(DbConnection connection) where M : BaseModel;

        M FindById<M>(M model) where M : BaseModel;

        M FindById<M>(M model, DbConnection connection) where M : BaseModel;

        void Add<M>(M model) where M : BaseModel;

        void Add<M>(M model, DbTransaction transaction) where M : BaseModel;

        void Update<M>(M model) where M : BaseModel;

        void Update<M>(M model, DbTransaction transaction) where M : BaseModel;

        void Delete<M>(M model) where M : BaseModel;

        void Delete<M>(M model, DbTransaction transaction) where M : BaseModel;

        public int Count();
    }
}
