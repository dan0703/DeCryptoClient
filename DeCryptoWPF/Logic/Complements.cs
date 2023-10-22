using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeCryptoWPF.Logic
{
    public class Complements
    {
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = new StringBuilder();
                for (int bit = 0; bit < (bytes.Length); bit++)
                {
                    hash.Append(bytes[bit].ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
}
