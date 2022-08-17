using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Core.Mapping
{
    public class Table
    {
        public string Name { get; set; }

        public IList<Column> Columns { get; set; }

        public Table(string name)
        {
            Name = name;
            Columns = new List<Column>();
        }

        public void AddColumn(Column column)
        {
            Console.WriteLine("##> " + column.Name);
            Columns.Add(column);
        }
    }
}
