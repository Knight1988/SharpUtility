using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpUtility.Common
{
    public static class AppDomainExentions
    {
        public static IEnumerable<TBase> LoadAssemblyAndCreateInstance<TBase>(this AppDomain domain, byte[] rawAssembly)
        {
            var assembly = domain.Load(rawAssembly);
            return CreateInstance<TBase>(domain, assembly);
        }

        public static IEnumerable<TBase> LoadAssemblyAndCreateInstance<TBase>(this AppDomain domain, AssemblyName assemblyRef)
        {
            var assembly = domain.Load(assemblyRef);
            return CreateInstance<TBase>(domain, assembly);
        }

        public static IEnumerable<TBase> LoadAssemblyAndCreateInstance<TBase>(this AppDomain domain, string assemblyPath)
        {
            var assembly = domain.Load(File.ReadAllBytes(assemblyPath));
            return CreateInstance<TBase>(domain, assembly);
        }

        private static IEnumerable<TBase> CreateInstance<TBase>(AppDomain domain, Assembly assembly)
        {
            var typeBase = typeof (TBase);
            var types = assembly.GetTypes()
                .Where(p => typeBase.IsAssignableFrom(p));

            return types.Select(type => (TBase) domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName));
        }
    }
}
