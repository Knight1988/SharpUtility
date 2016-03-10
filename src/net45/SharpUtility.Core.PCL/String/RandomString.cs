using System;
using System.Text;
using SharpUtility.Enum;

namespace SharpUtility.String
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
    }
}