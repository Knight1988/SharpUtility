using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpUtility.Core.PCL.Reflection
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetInheritedTypes(this Assembly assembly, Type baseType)
        {
            return from type in assembly.ExportedTypes
            where type.GetTypeInfo().IsSubclassOf(baseType)
            select type;
        }
    }
}
