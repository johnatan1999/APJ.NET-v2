using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models
{
    public class UserInfos : StateModel
    {

        public UserInfos()
        {
            this.SetTableName("UserInfos");
            this.SetIndicePk("");
        }

        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        private string _lastname;

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }


        private DateTime _dayofbirth;

        public DateTime DayOfBirth
        {
            get { return _dayofbirth; }
            set { _dayofbirth = value; }
        }


    }
}
