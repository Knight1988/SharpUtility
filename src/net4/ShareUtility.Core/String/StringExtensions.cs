using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShareUtility.Core.String
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
        /// Count number of words by listed char
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByListedChars(this string s)
        {
            var regex = new Regex(@"[^\s.?,]+", RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }

        /// <summary>
        /// Count number of words by alpha numberic
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WordCountByAlphaNumeric(this string s)
        {
            var regex = new Regex(@"[\w]+", RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return WordCountByRegex(s, regex);
        }
    }
}
