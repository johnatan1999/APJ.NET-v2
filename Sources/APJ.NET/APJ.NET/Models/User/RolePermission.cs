using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models.User
{
    public class RolePermission : BaseModel
    {
		private string _idRole;

		public string IdRole
		{
			get { return _idRole; }
			set { _idRole = value; }
		}

		private string _idPermission;

		public string IdPermission
		{
			get { return _idPermission; }
			set { _idPermission = value; }
		}

		private string _apiurl;

		public string ApiUrl
		{
			get { return _apiurl; }
			set { _apiurl = value; }
		}


		public RolePermission()
		{
			SetTableName("RolePermission");
			SetPkLength(10);
			SetIndicePk("RPRMS");
		}
	}
}
