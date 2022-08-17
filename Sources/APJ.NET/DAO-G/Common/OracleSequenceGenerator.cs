using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;

namespace Apj.Net.Dao.Common
{
    public class OracleSequenceGenerator : ISequenceGenerator
    {
        public int GetCurrentSequence(BaseModel model, DbTransaction transaction)
        {
            var sql = "SELECT " + model.GetSequenceName() + ".CURRVAL FROM DUAL";
            try
            {
                DbCommand cmd = new OracleCommand(sql,
                (OracleConnection)transaction.Connection,
                (OracleTransaction)transaction);
                var nextVal = cmd.ExecuteScalar();
                return nextVal != null ? Convert.ToInt32(nextVal) : -1;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("GetCurrentSequence $> " + sql);
                throw ex;
            } 
        }

        public int GetNextSequence(BaseModel model, DbTransaction transaction)
        {
            var sql = "SELECT " + model.GetSequenceName() + ".NEXTVAL FROM DUAL";
            try
            {
                DbCommand cmd = new OracleCommand(sql,
                    (OracleConnection)transaction.Connection,
                    (OracleTransaction)transaction);
                var nextVal = cmd.ExecuteScalar();
                return nextVal != null ? Convert.ToInt32(nextVal) : -1;
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetCurrentSequence $> " + sql);
                throw ex;
            }
        }
    }
}
