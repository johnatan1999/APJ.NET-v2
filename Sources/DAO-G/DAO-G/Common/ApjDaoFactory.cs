using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Common
{
    public static class ApjDaoFactory
    {
        private static IApjDao dao;

        public static IApjDao GetInstance()
        {
            if(dao == null)
            {
                switch (Config.DB_NAME)
                {
                    case Constante.ORACLE_DB:
                        break;
                    case Constante.SQLSERVER_DB:
                        dao = new SQLServerDAO();
                        break;
                    case Constante.POSTGRES_DB:
                        break;
                    default: break;
                }
            }
            return dao;
        }

    }
}
