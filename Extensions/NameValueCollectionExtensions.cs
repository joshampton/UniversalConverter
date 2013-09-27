using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace UniversalConverter
{
    public static class QueryStringExtensions
    {
        public static bool TryGetValue<T>(this NameValueCollection target, string key, out T value, StringComparison comparison)
        {
            bool success = false;
            value = default(T);

            if (target.AllKeys.Any(k => key.Equals(k, comparison)))
                success = target[key].TryConvert<T>(out value);

            return success;
        }

        public static bool TryGetValue<T>(this NameValueCollection target, string key, out T value)
        {
            return TryGetValue<T>(target, key, out value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
