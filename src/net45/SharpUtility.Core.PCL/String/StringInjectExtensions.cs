using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SharpUtility.Core.String
{
    public static class StringInjectExtension
    {
        private static IDictionary GetPropertyHash(object properties)
        {
            Dictionary<string, object> hashtables = null;
            if (properties != null)
            {
                
                hashtables = new Dictionary<string, object>();
                foreach (var property in properties.GetType().GetRuntimeProperties())
                {
                    hashtables.Add(property.Name, property.GetValue(properties));
                }
            }
            return hashtables;
        }

        public static string Inject(this string formatString, object injectionObject)
        {
            return formatString.Inject(GetPropertyHash(injectionObject));
        }

        //public static string Inject(this string formatString, IDictionary dictionary)
        //{
        //    return formatString.Inject(new Hashtable(dictionary));
        //}

        public static string Inject(this string formatString, IDictionary attributes)
        {
            string str;
            var str1 = formatString;
            if ((attributes != null && formatString != null))
            {
                foreach (string key in attributes.Keys)
                {
                    str1 = str1.InjectSingleValue(key, attributes[key]);
                }
                str = str1;
            }
            else
            {
                str = str1;
            }
            return str;
        }

        public static string InjectSingleValue(this string formatString, string key, object replacementValue)
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
                    object[] item = { match.Groups[2] };
                    var str2 = string.Format(invariantCulture, "{{0:{0}}}", item);
                    var currentCulture = CultureInfo.CurrentCulture;
                    item = new[] { replacementValue };
                    str1 = string.Format(currentCulture, str2, item);
                }
                str = str.Replace(match.ToString(), str1);
            }
            return str;
        }
    }
}