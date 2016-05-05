using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SharpUtility.Reflection
{
    public static class ObjectExtensions
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
    }
}