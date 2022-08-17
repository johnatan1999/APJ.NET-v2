using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Common
{
    public static class ApjSequenceGeneratorFactory
    {
        static ISequenceGenerator sequenceGenerator;
        
        public static ISequenceGenerator GetSequenceGenerator()
        {
            if(sequenceGenerator == null)
            {
                switch(Config.DB_NAME)
                {
                    case Constante.ORACLE_DB:
                        sequenceGenerator = new OracleSequenceGenerator();
                        break;
                    case Constante.SQLSERVER_DB:
                        sequenceGenerator = new SqlServerSequenceGenerator();
                        break;                    
                    case Constante.POSTGRES_DB:
                        sequenceGenerator = new PostgresSequenceGenerator();
                        break;
                    default: break;
                }
            }
            return sequenceGenerator;
        }
    }
}
