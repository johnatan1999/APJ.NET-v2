using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Actions
{
    public abstract class IAction
    {
        public abstract object Execute(string [] args);


        /// <summary>
        /// Rise an exception with the provided error message if the clause is true
        /// </summary>
        /// <param name="clause"></param>
        /// <param name="errorMessage"></param>
        public static void validate(bool clause, string errorMessage)
        {
            if (clause)
                throw new Exception(errorMessage);
        }
    }
}
