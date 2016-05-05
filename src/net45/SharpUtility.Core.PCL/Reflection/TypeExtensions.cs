using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpUtility.Reflection
{
    public static class TypeExtensions
    {
        public static Dictionary<Type, Attribute> GetPropertyAttributes(this Type type, string name, bool inherit = true)
        {
            return type.GetRuntimeProperty(name).GetCustomAttributes(inherit).ToDictionary(a => a.GetType(), a => a as Attribute);
        }

        public static Dictionary<Type, Attribute> GetFieldAttributes(this Type type, string name, bool inherit = true)
        {
            return type.GetRuntimeField(name).GetCustomAttributes(inherit).ToDictionary(a => a.GetType(), a => a as Attribute);
        }
    }
}
