using System;
using System.Text;
using SharpUtility.Enum;

namespace SharpUtility.String
{
    public class RandomString
    {
        protected string CharLimit { get; set; } = String.CharLimit.Default.GetStringValue();

        protected Random Random { get; set; } = new Random();

        /// <summary>
        /// Set the char limit so the randomed string will only contain those characters
        /// </summary>
        /// <param name="charLimits"></param>
        public void SetCharLimit(string charLimits)
        {
            CharLimit = charLimits;
        }

        /// <summary>
        /// Set the char limit so the randomed string will only contain those characters.
        /// You can combine lower and upper by using CharLimit.UpperCase | UpperCase.LowerCase
        /// </summary>
        /// <param name="charLimit"></param>
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
                limit.Append(String.CharLimit.UpperCase.GetStringValue());

            if (charLimit.HasFlag(String.CharLimit.LowerCase))
                limit.Append(String.CharLimit.LowerCase.GetStringValue());

            if (charLimit.HasFlag(String.CharLimit.Number))
                limit.Append(String.CharLimit.Number.GetStringValue());

            CharLimit = limit.ToString();
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
                stringChars[i] = CharLimit[Random.Next(CharLimit.Length)];

            var finalString = new string(stringChars);
            return finalString;
        }
    }
}