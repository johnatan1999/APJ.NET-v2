using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Utility
{
    internal class Util
    {
        public static string FirstLetterToUpper(string str)
        {
            var firstLetter = str[0].ToString().ToUpper();
            firstLetter += string.Join("", str.ToArray().Skip(1).ToArray());
            return firstLetter;
        }
    }
}
