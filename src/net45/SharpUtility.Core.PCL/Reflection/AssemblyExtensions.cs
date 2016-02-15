using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpUtility.Core.PCL.Reflection
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetInheritedTypes(this Assembly assembly, Type baseType)
        {
            return from type in assembly.ExportedTypes
                where baseType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo())
                select type;
        }
    }
}