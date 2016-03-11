using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpUtility.String
{
    public static class StringExtensions
    {
        /// <summary>
        /// Count number of words by regex
        /// </summary>
        /// <param name="s"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static int WordCountByRegex(this string s, Regex regex)
        {
            var matchesByListedChars = regex.Matches(s);
            return matchesByListedChars.Count;
        }

        /// <summary>
        /// Count number of words by listed char (It's = 1)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByListedChars(this string s)
        {
            var regex = new Regex(@"[^\s.?,]+", RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }

        /// <summary>
        /// Count number of words by alpha numberic (It's = 2)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByAlphaNumeric(this string s)
        {
            var regex = new Regex(@"[\w]+", RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }

        /// <summary>
        /// Convert hex string to byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Replace string with StringComparision
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            var sb = new StringBuilder();

            var previousIndex = 0;
            var index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        /// <summary>
        /// Containt with comparison type
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string s, string value, StringComparison comparisonType)
        {
            return s.IndexOf(value, comparisonType) > -1;
        }
    }
}
