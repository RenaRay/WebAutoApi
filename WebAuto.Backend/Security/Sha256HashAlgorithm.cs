using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAuto.Backend.Security
{
    public class Sha256HashAlgorithm : IHashAlgorithm
    {
        public string Hash(string input)
        {
            using (var cryptoServiceProvider = new SHA256CryptoServiceProvider())
            {
                byte[] byteValue = Encoding.UTF8.GetBytes(input);
                byte[] byteHash = cryptoServiceProvider.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}