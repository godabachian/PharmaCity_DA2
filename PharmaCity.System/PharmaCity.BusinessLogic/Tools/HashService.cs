using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic.Tools
{
    public class HashService
    {
        private const int _Iterations = 1000;
        private const int _SaltSize = 16;
        private const int _HashSize = 20;

        public string GetHash(string password)
        {
            byte[] salt = new byte[_SaltSize];
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _Iterations);
            var hash = pbkdf2.GetBytes(_HashSize);
            var hashedBase46String = Convert.ToBase64String(hash);

            return hashedBase46String;
        }
    }
}
