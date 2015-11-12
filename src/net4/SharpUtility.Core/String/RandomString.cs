using System;
using System.Security.Cryptography;
using System.Text;
using SharpUtility.Core.Enum;

namespace SharpUtility.Core.String
{
    public class RandomString
    {
        private string _charLimit = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private Random _random = new Random();

        public string CharLimit
        {
            get { return _charLimit; }
            set { _charLimit = value; }
        }

        public void SetCharLimit(CharLimit charLimit)
        {
            var limit = new StringBuilder();
            if (charLimit.HasFlag(String.CharLimit.Default))
            {
                limit.Append(String.CharLimit.Default.GetStringValue());
                CharLimit = limit.ToString();
                return;
            }

            if (charLimit.HasFlag(String.CharLimit.UpperCase))
            {
                limit.Append(String.CharLimit.UpperCase.GetStringValue());
            }

            if (charLimit.HasFlag(String.CharLimit.LowerCase))
            {
                limit.Append(String.CharLimit.LowerCase.GetStringValue());
            }

            if (charLimit.HasFlag(String.CharLimit.Number))
            {
                limit.Append(String.CharLimit.Number.GetStringValue());
            }

            CharLimit = limit.ToString();
        }

        public Random Random
        {
            get { return _random; }
            set { _random = value; }
        }

        /// <summary>
        ///     Get a random string in length
        /// </summary>
        /// <param name="length">length of random string</param>
        /// <returns>random string</returns>
        public string Next(int length)
        {
            var stringChars = new char[length];

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = CharLimit[Random.Next(CharLimit.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }

        /// <summary>
        ///     Get a secure random string using RNGCryptoServiceProvider
        /// </summary>
        /// <param name="length">length of random string</param>
        /// <returns>random string</returns>
        public string NextSecure(int length)
        {
            var chars = CharLimit.ToCharArray();
            var data = new byte[1];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[length];
                crypto.GetNonZeroBytes(data);
            }
            var result = new StringBuilder(length);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}