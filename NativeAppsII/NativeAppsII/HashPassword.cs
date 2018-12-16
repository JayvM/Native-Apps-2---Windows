using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII
{
    public class HashPassword
    {
        public static String Hashpassword(string password)
        {

            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            return Convert.ToBase64String(passwordBytes);
        }

        public static String GenerateSalt()
        {
            Byte[] salt = new byte[10];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
