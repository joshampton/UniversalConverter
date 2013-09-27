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
        private readonly IEnumerable<Type> supportedTypes;

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

        public bool TryConvert<T>(object source, out T result, IFormatProvider formatProvider = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            result = default(T);
            object temp = null;
            bool success = this.TryConvert(source.GetType(), typeof(T), source, out temp, formatProvider);

            if (success)
                result = (T)temp;

            return success;
        }

        public bool TryConvert<Source, Destination>(Source source, out Destination result, IFormatProvider formatProvider = null)
        {
            result = default(Destination);
            bool sucess = false;

            if (typeof(Source).CanBeNull() && typeof(Destination).CanBeNull() && source.IsNull())
            {
                sucess = true;
                result = default(Destination);
                return sucess;
            }
            else
            {
                object temp = null;
                sucess = TryConvert(typeof(Source), typeof(Destination), source, out temp, formatProvider);
                if (sucess)
                    result = (Destination)temp;
                return sucess;
            }
        }

        public object Convert(Type sourceType, Type destinationType, object source, IFormatProvider formatProvider = null)
        {
            object temp = null;

            this.TryConvert(sourceType, destinationType, source, out temp, formatProvider);

            return temp;
        }

        public T Convert<T>(object source, IFormatProvider formatProvider = null)
        {
            T result = default(T);

            this.TryConvert<T>(source, out result, formatProvider);

            return result;
        }

        public Destination Convert<Source, Destination>(Source source, IFormatProvider formatProvider = null)
        {
            Destination result = default(Destination);

            this.TryConvert<Source, Destination>(source, out result, formatProvider);

            return result;
        }
    }
}
