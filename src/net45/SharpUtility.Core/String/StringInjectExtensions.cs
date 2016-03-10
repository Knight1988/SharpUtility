using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SharpUtility.String
{
    public static class StringInjectExtension
    {
        private static Hashtable GetPropertyHash(object properties)
        {
            Hashtable hashtables = null;
            if (properties != null)
            {
                hashtables = new Hashtable();
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(properties))
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

        public static string Inject(this string formatString, IDictionary dictionary)
        {
            return formatString.Inject(new Hashtable(dictionary));
        }

        public static string Inject(this string formatString, Hashtable attributes)
        {
            string str;
            string str1 = formatString;
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
            string str = formatString;
            Regex regex = new Regex(string.Concat("{(", key, ")(?:}|(?::(.[^}]*)}))"));
            foreach (Match match in regex.Matches(formatString))
            {
                string str1;
                if (match.Groups[2].Length <= 0)
                {
                    object empty = replacementValue ?? string.Empty;
                    str1 = empty.ToString();
                }
                else
                {
                    CultureInfo invariantCulture = CultureInfo.InvariantCulture;
                    object[] item = { match.Groups[2] };
                    string str2 = string.Format(invariantCulture, "{{0:{0}}}", item);
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;
                    item = new[] { replacementValue };
                    str1 = string.Format(currentCulture, str2, item);
                }
                str = str.Replace(match.ToString(), str1);
            }
            return str;
        }
    }
}