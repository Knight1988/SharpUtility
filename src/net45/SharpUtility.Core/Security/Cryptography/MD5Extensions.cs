﻿using System.Security.Cryptography;
using System.Text;

namespace SharpUtility.Security.Cryptography
{
    // ReSharper disable once InconsistentNaming
    public static class MD5Extensions
    {
        public static string ComputeHash(this MD5 md5, string input, Encoding encoding)
        {
            // step 1, calculate MD5 hash from input
            var inputBytes = encoding.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ComputeHash(this MD5 md5, string input)
        {
            return ComputeHash(md5, input, Encoding.UTF8);
        }
    }
}