using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Services
{
    public class ImportService
    {

        public static void Import(BaseModel [] models)
        {
            DbConnection connection = ConnectionFactory.GetConnection();
            DbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                foreach(var m in models)
                {
                    m.Save(transaction);
                }
            }
            catch(Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

    }
}
