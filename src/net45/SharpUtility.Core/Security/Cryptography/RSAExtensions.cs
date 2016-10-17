using System.Security.Cryptography;
using System.Text;
using SharpUtility.StringManipulation;

namespace SharpUtility.Security.Cryptography
{
    public static class RSAExtensions
    {
        public static string Decrypt(this RSACryptoServiceProvider rsa, string input)
        {
            // Convert the string to byte array
            var encrypted = input.HexStringToByteArray();

            // Decrypt it using the private key
            var secretData = rsa.Decrypt(encrypted, true);

            return Encoding.UTF8.GetString(secretData);
        }

        public static string Encrypt(this RSACryptoServiceProvider rsa, string input)
        {
            // Convert the string to byte array
            var secretData = Encoding.UTF8.GetBytes(input);

            // Encrypt it using the public key
            var encrypted = rsa.Encrypt(secretData, true);

            // Convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in encrypted)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}