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
            if(target == null)
                throw new ArgumentNullException("target");

            bool success = false;

            object tempResult = default(T);

            lock (baton)
                success = converter.TryConvert(target.GetType(), typeof(T), target, out tempResult, formatProvider);

            result = (T)tempResult;

            return success;
        }

        public static bool TryConvert<Source, Destination>(this Source target, out Destination result, IFormatProvider formatProvider = null)
        {
            bool success = false;

            object tempResult = default(Destination);

            lock (baton)
                success = converter.TryConvert(typeof(Source), typeof(Destination), target, out tempResult, formatProvider);

            result = (Destination)tempResult;

            return success;
        }
    }
}
