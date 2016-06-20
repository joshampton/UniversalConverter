using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace UniversalConverter
{
    public sealed class IConvertibleConverter : IConverter
    {
        private readonly IEnumerable<Type> supportedDestinationTypes;

        public IConvertibleConverter()
        {
            supportedDestinationTypes = new List<Type> 
            {
                typeof(bool),
                typeof(byte),
                typeof(char),
                typeof(DateTime),
                typeof(decimal),
                typeof(double),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(string),
                typeof(ushort),
                typeof(uint),
                typeof(ulong)
            };
        }

        public bool TryConvert(ConverterContext context, out object result)
        {
            bool success = false;
            result = null;

            if (!supportedDestinationTypes.Contains(context.DestinationType) || typeof(IConvertible).IsAssignableFrom(context.SourceType)) return success;

            try
            {
                result = ((IConvertible)context.Source).ToType(context.DestinationType, CultureInfo.CurrentCulture);
                success = true;
            }
            catch { }

            return success;
        }
    }
}
