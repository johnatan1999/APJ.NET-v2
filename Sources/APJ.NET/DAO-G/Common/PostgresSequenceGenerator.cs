using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common
{
    public class PostgresSequenceGenerator : ISequenceGenerator
    {
        public int GetCurrentSequence(BaseModel model, DbTransaction transaction)
        {
            return 0;
        }

        public int GetNextSequence(BaseModel model, DbTransaction transaction)
        {
            return 0;
        }
    }
}
