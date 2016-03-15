using System.Security.Cryptography;
using System.Text;

namespace SharpUtility.Security.Cryptography
{
    // ReSharper disable once InconsistentNaming
    public static class SHA1Extensions
    {
        public static string ComputeHash(this SHA1 sha1, string input, Encoding encoding)
        {
            // step 1, calculate SHA1 hash from input
            var inputBytes = encoding.GetBytes(input);
            var hash = sha1.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ComputeHash(this SHA1 sha1, string input)
        {
            return ComputeHash(sha1, input, Encoding.UTF8);
        }
    }
}