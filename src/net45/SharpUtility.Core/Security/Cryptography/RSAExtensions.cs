using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SharpUtility.Core.String;

namespace SharpUtility.Core.Security.Cryptography
{
    public static class RSAExtensions
    {
        public static string Encrypt(this RSACryptoServiceProvider rsa, string input)
        {
            // Convert the string to byte array
            byte[] secretData = Encoding.UTF8.GetBytes(input);

            // Encrypt it using the public key
            byte[] encrypted = rsa.Encrypt(secretData, true);

            // Convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in encrypted)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string Decrypt(this RSACryptoServiceProvider rsa, string input)
        {
            // Convert the string to byte array
            byte[] encrypted = input.HexStringToByteArray();

            // Decrypt it using the private key
            byte[] secretData = rsa.Decrypt(encrypted, true);

            return Encoding.UTF8.GetString(secretData);
        }
    }
}
