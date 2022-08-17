using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Model
{
    internal class ApjTest : BaseModel
    {

        public ApjTest()
        {
            SetTableName("Roles");
            SetPkLength(10);
            SetIndicePk("APJ");
        }

        public virtual string Name { get; set; }


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
