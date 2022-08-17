using Apj.Net.Dao.Connection;
using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Apj.Net.Dao.Common
{
    class SqlServerSequenceGenerator : ISequenceGenerator
    {
        public int GetCurrentSequence(BaseModel model, DbTransaction transaction)
        {
            var sql = "SELECT current_value FROM sys.sequences WHERE name = @SeqName";
            DbCommand cmd = new SqlCommand(sql, 
                (SqlConnection)transaction.Connection, 
                (SqlTransaction)transaction);
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = "@SeqName";
            parameter.Value = model.GetSequenceName();
            cmd.Parameters.Add(parameter);
            var nextVal = cmd.ExecuteScalar();
            return nextVal != null ? Convert.ToInt32(nextVal) : -1;
        }

        public int GetNextSequence(BaseModel model, DbTransaction transaction)
        {
            var sql = "SELECT NEXT VALUE FOR " + model.GetSequenceName();
            DbCommand cmd = new SqlCommand(sql, 
                (SqlConnection)transaction.Connection,
                (SqlTransaction)transaction);
            var nextVal = cmd.ExecuteScalar();
            return nextVal != null ? Convert.ToInt32(nextVal) : -1;
        }
    }
}
