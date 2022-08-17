using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace APJ.NET.Models.User
{
    public class Role : BaseModelHistory
    {
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Role()
        {
            SetTableName("Roles");
            SetPkLength(10);
            SetIndicePk("ROLE");
        }
    }

}
