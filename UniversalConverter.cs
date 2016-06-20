using System;
using System.Collections.Generic;
using System.Globalization;

namespace UniversalConverter
{
    public sealed class UniversalConverter
    {
        private readonly IEnumerable<IConverter> converters;
        private readonly IDictionary<ConverterKey, IConverter> cachedConverters;

        public UniversalConverter()
        {
            cachedConverters = new Dictionary<ConverterKey, IConverter>();
            converters = new List<IConverter> 
            {
                new IConvertibleConverter(),
                new TypeConverterConverter()
            };
        }

        public bool TryConvert(Type sourceType, Type destinationType, object source, out object result, IFormatProvider formatProvider = null)
        {
            //bool successful = false;
            //result = null;

            //if (sourceType.IsNullable()) sourceType = Nullable.GetUnderlyingType(sourceType);

            //if (destinationType.IsNullable()) destinationType = Nullable.GetUnderlyingType(destinationType);

            //if (source == null)
            //{
            //    result = destinationType.GetDefaultValue();
            //    successful = true;
            //    return successful;
            //}

            //var context = new ConverterContext(sourceType, destinationType, source, formatProvider ?? CultureInfo.CurrentCulture);

            //IConverter cachedConverter = null;
            //ConverterResult converterResult = null;
            //if (cachedConverters.TryGetValue(context.Key, out cachedConverter))
            //{
            //    converterResult = cachedConverter.TryConvert(context);
            //}
            //else
            //{
            //    foreach (var converter in converters)
            //    {
            //        if (successful = converter.TryConvert(context, out result))
            //        {
            //            cachedConverters[context.Key] = converter;
            //            break;
            //        }
            //    }
            //}

            //return successful;

            throw new NotImplementedException();
        }
    }
}
