using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SharpUtility.StringManipulation
{
    public static class StringInjectExtension
    {
        private static IDictionary GetPropertyHash(object properties)
        {
            return properties?.GetType()
                .GetRuntimeProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(properties));
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation
        ///     of a corresponding properties in a specified object.
        /// </summary>
        /// <param name="formatString">A composite format string.</param>
        /// <param name="injectionObjects">An array object that contain property to format</param>
        /// <returns></returns>
        public static string Inject(this string formatString, params object[] injectionObjects)
        {
            return injectionObjects.Aggregate(formatString, Inject);
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation
        ///     of a corresponding properties in a specified object.
        /// </summary>
        /// <param name="formatString">A composite format string.</param>
        /// <param name="injectionObject">A object that contain property to format</param>
        /// <returns></returns>
        public static string Inject(this string formatString, object injectionObject)
        {
            return formatString.Inject(GetPropertyHash(injectionObject));
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation
        ///     of a corresponding value in a specified key.
        /// </summary>
        /// <param name="formatString">A composite format string.</param>
        /// <param name="attributes">A dictionary that contain key and value to format</param>
        /// <returns></returns>
        public static string Inject(this string formatString, IDictionary attributes)
        {
            return attributes.Keys.Cast<string>().Aggregate(formatString, (current, key) => Inject(current, key, attributes[key]));
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation
        ///     of a corresponding value in a specified key.
        /// </summary>
        /// <param name="formatString">A composite format string.</param>
        /// <param name="key">key to replace</param>
        /// <param name="replacementValue">value to replace</param>
        /// <returns></returns>
        public static string Inject(this string formatString, string key, object replacementValue)
        {
            var str = formatString;
            var regex = new Regex(string.Concat("{(", key, ")(?:}|(?::(.[^}]*)}))"));
            foreach (Match match in regex.Matches(formatString))
            {
                string str1;
                if (match.Groups[2].Length <= 0)
                {
                    var empty = replacementValue ?? string.Empty;
                    str1 = empty.ToString();
                }
                else
                {
                    var invariantCulture = CultureInfo.InvariantCulture;
                    object[] item = {match.Groups[2]};
                    var str2 = string.Format(invariantCulture, "{{0:{0}}}", item);
                    var currentCulture = CultureInfo.CurrentCulture;
                    item = new[] {replacementValue};
                    str1 = string.Format(currentCulture, str2, item);
                }
                str = str.Replace(match.ToString(), str1);
            }
            return str;
        }
    }
}