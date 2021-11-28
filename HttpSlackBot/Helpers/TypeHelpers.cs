using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HttpSlackBot.Helpers
{
    public static class TypeHelpers
    {
        public static bool IsRealClass(this Type testType)
        {
            return testType.IsAbstract == false
                    && testType.IsGenericTypeDefinition == false
                    && testType.IsInterface == false;
        }

        public static IEnumerable<Type> GetTypesInNamespace<T>(this Assembly assembly, string targetNamespace)
        {
            return assembly.GetTypes()
                           .Where(x => x.Namespace != null && x.Namespace.Equals(targetNamespace, StringComparison.OrdinalIgnoreCase))
                           .Where(x => typeof(T).IsAssignableFrom(x))
                           .Where(x => x.IsRealClass());
        }

    }
}
