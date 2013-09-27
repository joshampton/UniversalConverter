using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConverter
{
    public static class ConversionExtensions
    {
        private static object baton = new object();
        private static UniversalConverter converter = new UniversalConverter();

        public static bool TryConvert<T>(this object target, out T result, IFormatProvider formatProvider = null)
        {
            bool success = false;

            lock (baton)
                success = converter.TryConvert(target, out result, formatProvider);

            return success;
        }

        public static bool TryConvert<Source, Destination>(this Source target, out Destination result, IFormatProvider formatProvider = null)
        {
            bool success = false;

            lock (baton)
                success = converter.TryConvert<Source, Destination>(target, out result, formatProvider);

            return success;
        }
    }
}
