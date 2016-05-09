using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace SharpUtility.Reflection
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        ///     Clone an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static Dictionary<Type, Attribute> GetPropertyAttributes(this object obj, string name, bool inherit = true)
        {
            return obj.GetType().GetPropertyAttributes(name, inherit);
        }

        public static Dictionary<Type, Attribute> GetFieldAttributes(this object obj, string name, bool inherit = true)
        {
            return obj.GetType().GetFieldAttributes(name, inherit);
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <param name="formatting">formatting option</param>
        /// <returns></returns>
        public static string ToJson(this object source, Formatting formatting)
        {
            return JsonConvert.SerializeObject(source, formatting);
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <param name="formatting">formatting option</param>
        /// <param name="settings">setting for serialized object</param>
        /// <returns></returns>
        public static string ToJson(this object source, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(source, formatting, settings);
        }
    }
}