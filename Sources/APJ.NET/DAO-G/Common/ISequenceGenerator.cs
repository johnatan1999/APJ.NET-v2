using Apj.Net.Dao.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Apj.Net.Dao.Common
{
    public interface ISequenceGenerator
    {
        public int GetNextSequence(BaseModel model, DbTransaction transaction);

        public int GetCurrentSequence(BaseModel model, DbTransaction transaction);
    }
}
