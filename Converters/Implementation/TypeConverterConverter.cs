using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UniversalConverter
{
    public sealed class TypeConverterConverter : IConverter
    {
        private readonly IDictionary<Type, TypeConverter> converterCache;

        public TypeConverterConverter()
        {
            this.converterCache = new Dictionary<Type, TypeConverter>();
        }

        private TypeConverter GetConverter(Type type)
        {
            TypeConverter converter = null;

            if (!converterCache.TryGetValue(type, out converter))
                converter = converterCache[type] = TypeDescriptor.GetConverter(type);

            return converter;
        }

        public bool TryConvert(ConverterContext context, out object result)
        {
            bool success = false;
            result = null;

            TypeConverter converter = GetConverter(context.DestinationType);

            try
            {
                result = converter.ConvertFrom(context.Source);
                success = true;
            }
            catch { }

            return success;
        }
    }
}
