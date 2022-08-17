using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Core.Mapping
{
    public class ApjORM
    {

        public static string ConvertTableToClass(string tableName)
        {
            Table table = DbAccess.MapTable(tableName);
            string template = Utility.FileAccess.GetTemplate("Model.txt");
            string attributes = "";
            IList<Column> columns = table.Columns;
            int i = 0;
            foreach (Column column in columns)
            {
                if(column.Name.ToLower().Equals("state")) continue;
                string type = column.Type.Name;
                switch (type)
                {
                    case "Int32": type = "int"; break;
                    default: break;
                }
                if (i > 0) attributes += "\t\t";
                i++;
                attributes += "public " + column.Type.Name + " " + column.Name + " { get; set; }\n"; 
            }
            template = template.Replace("/*Attributes*/", attributes);
            return template;
        }

        public static string ConvertTableToClass(string tableName, string modelTemplate)
        {
            Table table = DbAccess.MapTable(tableName);
            string attributes = "";
            IList<Column> columns = table.Columns;
            int i = 0; 
            foreach(Column column in columns)
            {
                string type = column.Type.Name;
                switch(type)
                {
                    case "Int32": type = "int"; break;
                    case "Decimal": type = "double"; break;
                    case "Byte": type = "DateTime"; break;
                    default: break;
                }
                if (i > 0) attributes += "\t\t";
                i++;
                attributes += "public " + type + " " + column.Name + " { get; set; }\n"; 
            }
            modelTemplate = modelTemplate.Replace("/*Attributes*/", attributes);
            return modelTemplate;
        }
    
    }
}
