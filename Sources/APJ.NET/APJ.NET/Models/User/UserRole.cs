using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace APJ.NET.Models.User
{
    public class UserRole : BaseModel
    {

        private string _idsuer;       

        public string IdUser
        {
            get { return _idsuer; }
            set { _idsuer = value; }
        }


        private string _idRole;

        public string IdRole
        {
            get { return _idRole; }
            set { _idRole = value; }
        }


        private int _rang;

        public int Rang
        {
            get { return _rang; }
            set { _rang = value; }
        }

        public UserRole()
        {
            SetTableName("UtilisateurRole");
        }

    }
}
