using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core.Page
{
    public class ListMetaData : MetaData
    {
        public readonly Dictionary<string, Column> Columns;

        public ListMetaData(): base()
        {
            Columns = new Dictionary<string, Column>();
        }

        public void AddColumn(string name, string label)
        {
            Columns.Add(name, new Column() { 
                AttributeName = name,
                Label = label
            });
        }

        public void SetLabel(string name, string label)
        {
            var column = Columns[name];
            column.Label = label;
        } 

    }

    public class Column
    {
        public string AttributeName { get; set; }
        public string Label { get; set; }
    }
}
