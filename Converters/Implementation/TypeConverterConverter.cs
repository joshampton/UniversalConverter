using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UniversalConverter
{
    public sealed class TypeConverterConverter : IConverter
    {
        private readonly IDictionary<Type, TypeConverter> converterCache;

        public TypeConverterConverter()
        {
            converterCache = new Dictionary<Type, TypeConverter>();
        }

        private TypeConverter GetConverter(Type type)
        {
            TypeConverter converter = null;

            if (!converterCache.TryGetValue(type, out converter)) converter = converterCache[type] = TypeDescriptor.GetConverter(type);

            return converter;
        }

        public ConverterResult Convert(ConverterContext context)
        {
            ConverterResult result = null;

            var converter = GetConverter(context.DestinationType);

            try
            {
                result = new ConverterResult(
                    result: converter.ConvertFrom(context.Source),
                    success: true);
            }
            catch (Exception ex)
            {
                result = new ConverterResult(
                    exception: ex,
                    success: false);
            }

            return result;
        }
    }
}
