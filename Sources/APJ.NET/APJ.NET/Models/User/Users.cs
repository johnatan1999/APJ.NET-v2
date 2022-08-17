using Apj.Net.Dao.Annotations;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace APJ.NET.Models.User
{
    public class Users : StateModelHistory
    {
        #region Properties
        private string _login;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _idrole;

        [ForeignKey("Role", tableName ="Roles")]
        public string IdRole
        {
            get { return _idrole; }
            set { _idrole = value; }
        }


        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        #endregion
        public Users()
        {
            SetIndicePk("USER");
            SetPkLength(10);
            SetTableName("Utilisateur");
        }
    }
}
