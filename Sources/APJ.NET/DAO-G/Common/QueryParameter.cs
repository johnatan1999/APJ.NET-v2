using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Common
{
    internal class QueryParameter
    {
        public static string GetQueryParameterID()
        {
            switch (Config.DB_NAME)
            {
                case Constante.SQLSERVER_DB:
                    return Constante.SQLSERVER_QUERY_PARAMETER_ID;
                case Constante.ORACLE_DB:
                    return Constante.ORACLE_QUERY_PARAMETER_ID;
                default: return "?";
            }
        }
    }
}
