using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Configuration
{
    public static class ConfigSectionRegistry
    {
        public static IEnumerable<Type> All => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(GetLoadableTypes)
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IConfigSection).IsAssignableFrom(t));

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null)!;
            }
        }
    }
}
