using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models.User
{
    public class Permission : BaseModel
    {
		private string _apiurl;

		public string ApiUrl
		{
			get { return _apiurl; }
			set { _apiurl = value; }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public Permission()
		{
			SetTableName("Permission");
			SetPkLength(10);
			SetIndicePk("PRMS");
		}

	}
}
