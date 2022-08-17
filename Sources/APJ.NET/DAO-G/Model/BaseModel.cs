using Apj.Net.Dao.Common;
using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Util;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Model
{
    public abstract class BaseModel
    {
		private string _id;

		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int PkLength;

		private string IndicePk="";

		private string TableName;

		private string SequenceName;

		public BaseModel(){ }

		public int GetPkLength()
		{
			return PkLength;
		}

		public void SetPkLength(int pkLength)
		{
			PkLength = pkLength;
		}

		public string GetIndicePK()
		{
			return IndicePk;
		}

		public void SetIndicePk(string indicePk)
		{
			IndicePk = indicePk;
		}

		public string GetTableName()
		{
			return TableName;
		}

		public void SetTableName(string tableName)
		{
			TableName = tableName;
			if (SequenceName == null) SequenceName = tableName + "_Seq";
		}

		public bool IsValide()
		{
			return true;
		}

		public void SetSequenceName(string seq)
		{
			SequenceName = seq;
		}

		public string GetSequenceName()
		{
			return SequenceName;
		}

		public string GetAttributIDName()
		{
			return "Id";
		}

		public void Save()
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Add(this);
		}

		public void Save(DbTransaction transaction)
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Add(this, transaction);
		}
		public void Update()
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Update(this);
		}

		public void Update(DbTransaction transaction)
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Update(this, transaction);
		}

		public void Delete()
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Delete(this);
		}

		public void Delete(DbTransaction transaction)
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.Delete(this, transaction);
		}
		
		public void Load()
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.FindById(this);
		}

		public void Load(DbConnection connection)
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.FindById(this, connection);
			//PropertyInfo[] properties = _this.GetType().GetProperties();
			//foreach(PropertyInfo p in properties)
			//{
			//	p.SetValue(this, p.GetValue(_this));
			//}
		}

		public void LoadReference()
		{
			DbConnection connection = ConnectionFactory.GetConnection();
			try 
			{
				connection.Open();
				List<BaseModel> references = AnnotationUtil.GetFKReference(this);
				foreach(var obj in references)
				{
					obj.Load(connection);
				}
			} 
			finally
			{
				if (connection != null) connection.Close();
			}
		}

		public void LoadReference(DbConnection connection)
		{
			List<BaseModel> references = AnnotationUtil.GetFKReference(this);
			foreach(var obj in references)
			{
				obj.Load(connection);
			}
		}

	}
}
