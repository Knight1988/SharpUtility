using System;
using System.Text;
using SharpUtility.Enum;

namespace SharpUtility.StringManipulation
{
    public class RandomString
    {
        public string CharLimit { get; set; } = StringManipulation.CharLimit.Default.GetStringValue();

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
            if (charLimit.HasFlag(StringManipulation.CharLimit.Default))
            {
                limit.Append(StringManipulation.CharLimit.Default.GetStringValue());
                CharLimit = limit.ToString();
                return;
            }

            if (charLimit.HasFlag(StringManipulation.CharLimit.UpperCase))
                limit.Append(StringManipulation.CharLimit.UpperCase.GetStringValue());

            if (charLimit.HasFlag(StringManipulation.CharLimit.LowerCase))
                limit.Append(StringManipulation.CharLimit.LowerCase.GetStringValue());

            if (charLimit.HasFlag(StringManipulation.CharLimit.Number))
                limit.Append(StringManipulation.CharLimit.Number.GetStringValue());

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