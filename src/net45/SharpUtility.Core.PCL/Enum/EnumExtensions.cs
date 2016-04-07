using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpUtility.String;

namespace SharpUtility.Enum
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get string value from enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this System.Enum value)
        {
            var attrs = value.GetCustomAttributes<StringValue>().FirstOrDefault();
            return attrs?.Value;
        }

        /// <summary>
        /// Get enum attributes
        /// </summary>
        /// <param name="value">enum value</param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this System.Enum value)
        {
            var type = value.GetType();
            var fi = type.GetRuntimeField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();

            return attrs;
        }
    }
}
