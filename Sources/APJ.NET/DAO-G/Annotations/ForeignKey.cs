using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Annotations
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        public string name { get; private set; }

        public string tableName { get; set; }

        public ForeignKey(string name)
        {
            this.name = name;
        }
        
        public ForeignKey(string name, string tableName)
        {
            this.name = name;
            tableName = tableName;
        }

    }
}
