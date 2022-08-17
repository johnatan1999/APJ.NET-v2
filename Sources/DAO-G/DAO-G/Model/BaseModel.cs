using Apj.Net.Dao.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Model
{
    public abstract class BaseModel
    {
		private String _id;

		public String Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int PkLength;

		private string IndicePk;

		private string TableName;

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
		}

		public bool IsValide()
		{
			return true;
		}

		public string GetSequenceName()
		{
			return GetTableName() + "_Seq";
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
			Console.WriteLine(this.GetTableName());
			dao.FindById(this);
		}

		public void Load(DbTransaction transaction)
		{
			IApjDao dao = ApjDaoFactory.GetInstance();
			dao.FindById(this, transaction);
			//PropertyInfo[] properties = _this.GetType().GetProperties();
			//foreach(PropertyInfo p in properties)
			//{
			//	p.SetValue(this, p.GetValue(_this));
			//}
		}

	}
}
