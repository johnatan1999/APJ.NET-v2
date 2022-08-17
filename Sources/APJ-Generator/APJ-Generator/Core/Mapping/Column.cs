using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Core.Mapping
{
    public class Column
    {

        private string _name;

        public string Name
        {
            get { return _name; }
            set {
                value = value.ToUpper();
                _name = value.Substring(0, 1) + value.Substring(1).ToLower(); 
            }
        }


        public Type Type { get; set; }

    }
}
