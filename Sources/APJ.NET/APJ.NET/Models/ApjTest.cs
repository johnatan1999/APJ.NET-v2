using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Models
{
    public class ApjTest : StateModel
    {

        public ApjTest()
        {
            this.SetTableName("ApjTest");
            this.SetIndicePk("");
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }
}
