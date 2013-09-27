using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConverter
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return target.IsValueType ? Activator.CreateInstance(target) : null;
        }

        public static bool IsNullable(this Type target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return target.IsGenericType && target.GetGenericTypeDefinition() == typeof(Nullable<>).GetGenericTypeDefinition();
        }

        public static bool CanBeNull(this Type target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return !target.IsValueType || target.IsNullable();
        }
    }
}
