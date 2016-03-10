﻿using System.Security.Cryptography;
using System.Text;

namespace SharpUtility.Security.Cryptography
{
    public static class MD5Extensions
    {
        public static string ComputeHash(this MD5 md5, string input)
        {
            // step 1, calculate MD5 hash from input
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}