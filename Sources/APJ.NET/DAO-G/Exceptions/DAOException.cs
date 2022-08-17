using System;
using System.Collections.Generic;
using System.Text;

namespace Apj.Net.Dao.Exceptions
{
    public class DAOException : Exception
    {
        public DAOException() { }
        public DAOException(string message) : base(message) { }
        public DAOException(string message, Exception ex) : base(message, ex){ }

    }
}
