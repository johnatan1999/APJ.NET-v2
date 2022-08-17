using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Core.Mapping
{
    public class DbAccess
    {
        static DbConnection GetConnection()
        {
            var connectionString = Environment.GetEnvironmentVariable("APJ_ORM_CNX_STRING");
            if(connectionString == null)
            {
                throw new Exception("Please specify the database connection string.\n\tSET APJ_ORM_CNX_STRING=<Connection_String>");
            }
            return new SqlConnection(connectionString);
        }
 

        public static Table MapTable(string tableName)
        {
            DbConnection cnx = GetConnection();
            try
            {
                cnx.Open();
                DbCommand cmd = cnx.CreateCommand();
                cmd.CommandText = "select * from " + tableName;
                DbDataReader rdr = cmd.ExecuteReader();
                int i = 0;
                Table table = new Table(tableName);
                int fieldCount = rdr.FieldCount;
                while(i < fieldCount)
                {
                    var column = new Column();
                    column.Name = rdr.GetName(i);
                    if(!column.Name.ToLower().Equals("id")) 
                    {
                        column.Type = rdr.GetFieldType(i);
                        table.AddColumn(column);
                    }
                    i++;
                }
                return table;
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
                throw ex;
            }
            finally
            {
                if (cnx != null) cnx.Close();
            }
        }
    }
}
