using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpUtility.StringManipulation
{
    public static class StringExtension
    {
        /// <summary>
        ///     Containt with comparison type
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string s, string value, StringComparison comparisonType)
        {
            return s.IndexOf(value, comparisonType) > -1;
        }

        /// <summary>
        ///     Convert hex string to byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        /// <summary>
        ///     Replace string with StringComparision
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
        ///     Count number of words by alpha numberic (It's = 2)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByAlphaNumeric(this string s)
        {
            var regex = new Regex(@"[\w]+",
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }

        /// <summary>
        ///     Count number of words by listed char (It's = 1)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByListedChars(this string s)
        {
            var regex = new Regex(@"[^\s.?,]+",
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }

        /// <summary>
        ///     Count number of words by regex
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
        ///     Trim string, double space, convert special white-space to normal white-space.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimSpaces(this string s)
        {
            var sb = new StringBuilder(s);
            var charCodes = new[]
            {
                " ",    // alt + 255
                " ",    // atl + 0160,
                "​",     // CharCode: 8203
            };
            // replace special white-space
            sb.Replace(charCodes, " ");
            // remove leading & trailling white-space
            s = sb.ToString().Trim();
            // remove double white-space
            s = Regex.Replace(s, @"\s+", " ");
            return s;
        }

        /// <summary>
        /// Replace all character array to new value
        /// </summary>
        /// <param name="sb">string builder to replace</param>
        /// <param name="chars">character array</param>
        /// <param name="newValue">new value to replace</param>
        /// <returns></returns>
        public static StringBuilder Replace(this StringBuilder sb, IEnumerable<char> chars, char newValue)
        {
            foreach (var c in chars)
            {
                sb.Replace(c, newValue);
            }
            return sb;
        }

        /// <summary>
        /// Replace all character array to new value
        /// </summary>
        /// <param name="sb">string builder to replace</param>
        /// <param name="strings">string array</param>
        /// <param name="newValue">new value to replace</param>
        /// <returns></returns>
        public static StringBuilder Replace(this StringBuilder sb, IEnumerable<string> strings, string newValue)
        {
            foreach (var s in strings)
            {
                sb.Replace(s, newValue);
            }
            return sb;
        }

        /// <summary>
        /// Trim string
        /// </summary>
        /// <param name="s">string to trim</param>
        /// <param name="trimStart">trim start</param>
        /// <param name="trimEnd">trim end</param>
        /// <param name="stringComparison">string comparion method</param>
        /// <returns>trimmed string</returns>
        public static string Trim(this string s, string trimStart, string trimEnd, StringComparison stringComparison)
        {
            s = s.TrimSpaces();
            if (s.StartsWith(trimStart, stringComparison)) s = s.Remove(0, trimStart.Length);
            if (s.EndsWith(trimEnd, stringComparison)) s = s.Remove(s.Length - trimEnd.Length, trimEnd.Length);

            return s;
        }

        /// <summary>
        /// Trim string
        /// </summary>
        /// <param name="s">string to trim</param>
        /// <param name="trimStart">trim start</param>
        /// <param name="trimEnd">trim end</param>
        /// <returns>trimmed string</returns>
        public static string Trim(this string s, string trimStart, string trimEnd)
        {
            return s.Trim(trimStart, trimEnd, StringComparison.Ordinal);
        }    
    }
}