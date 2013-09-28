using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace UniversalConverter
{
    public class UniversalConverter
    {
        private readonly IEnumerable<IConverter> converters;
        private readonly IDictionary<ConverterKey, IConverter> cachedConverters;

        public UniversalConverter()
        {
            this.cachedConverters = new Dictionary<ConverterKey, IConverter>();
            this.converters = new List<IConverter> 
            {
                new IConvertibleConverter(),
                new TypeConverterConverter()
            };
        }

        public bool TryConvert(Type sourceType, Type destinationType, object source, out object result, IFormatProvider formatProvider = null)
        {
            bool successful = false;
            result = null;

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            if (sourceType.IsNullable())
                sourceType = Nullable.GetUnderlyingType(sourceType);

            if (destinationType.IsNullable())
                destinationType = Nullable.GetUnderlyingType(destinationType);

            if (source == null)
            {
                result = destinationType.GetDefaultValue();
                successful = true;
                return successful;
            }

            var context = new ConverterContext(sourceType, destinationType, source, formatProvider);

            IConverter cachedConverter = null;
            if (this.cachedConverters.TryGetValue(context.Key, out cachedConverter))
            {
                successful = cachedConverter.TryConvert(context, out result);
            }
            else
            {
                foreach (var converter in this.converters)
                {
                    if (successful = converter.TryConvert(context, out result))
                    {
                        this.cachedConverters[context.Key] = converter;
                        break;
                    }
                }
            }

            return successful;
        }
    }
}
