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
            supportedDestinationTypes = new Type[]
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

        public ConverterResult Convert(ConverterContext context)
        {
            ConverterResult result = null;

            if (!supportedDestinationTypes.Contains(context.DestinationType) || typeof(IConvertible).IsAssignableFrom(context.SourceType))
            {
                result = new ConverterResult(success: false);
            }
            else
            {
                try
                {
                    result = new ConverterResult(
                        result: ((IConvertible)context.Source).ToType(context.DestinationType, context.FormatProvider),
                        success: true
                    );
                }
                catch (Exception ex)
                {
                    result = new ConverterResult(
                        exception: ex,
                        success: false
                    );
                }
            }

            return result;
        }
    }
}