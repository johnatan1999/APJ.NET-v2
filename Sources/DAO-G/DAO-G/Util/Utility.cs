using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Apj.Net.Dao.Util
{
    public static class Utility
    {

        public static string ReplaceMultiplceSpacingByOne(string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }

    }
}
