using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Apj.Net.Dao.Util
{
    internal static class ApjFieldUtil
    {
        public static DbType GetPropertyDbType(Type p) 
        {
            switch (p.Name)
            {
                case "String":
                    return DbType.StringFixedLength;
                case "Int32":
                case "UInt32":
                    return DbType.Int32;
                case "Int16":
                case "UInt16":
                    return DbType.Int16;
                case "SByte":
                case "Byte":
                    return DbType.Byte;
                default:
                    return new DbType();
            }
        }  
    }
}
