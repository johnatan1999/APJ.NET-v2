using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Model
{
    public class ApjTest : StateModel
    {

        public ApjTest()
        {
            SetTableName("ApjTest");
            SetPkLength(10);
            SetIndicePk("APJ");
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private String _description;

        public String Description
        {
            get { return _description; }
            set
            {
                if (value.Length == 100)
                    throw new Exception("Valeur invalide");
                _description = value;
            }
        }

        //private string _adress;

        //public string Adress
        //{
        //    get { return _adress; }
        //    set { _adress = value; }
        //}

    }
}
