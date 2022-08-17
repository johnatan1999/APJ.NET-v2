using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APJ.NET.Util
{
    public class EncryptionUtil
    {
        public static string Encrypt(string str)
        {
            var sha = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(str);
            return Encoding.ASCII.GetString(sha.ComputeHash(data));
        }

        public static bool Verify(string str, string encryptedStr)
        {
            return Encrypt(str).Equals(encryptedStr);
        }
    }
}
