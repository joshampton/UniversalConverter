using System;

namespace UniversalConverter
{
    public sealed class ConverterContext : IEquatable<ConverterContext>
    {
        private readonly IFormatProvider formatProvider;
        private readonly Type sourceType;
        private readonly Type destinationType;
        private readonly object source;
        private readonly ConverterKey key;

        public Type SourceType { get { return sourceType; } }
        public Type DestinationType { get { return destinationType; } }
        public IFormatProvider FormatProvider { get { return formatProvider; } }
        public object Source { get { return source; } }
        public ConverterKey Key { get { return key; } }

        public ConverterContext(Type sourceType, Type destinationType, object source, IFormatProvider formatProvider)
        {
            if (sourceType == null) throw new ArgumentNullException("sourceType");

            if (destinationType == null) throw new ArgumentNullException("destinationType");

            if (formatProvider == null) throw new ArgumentNullException("provider");

            this.sourceType = sourceType;
            this.destinationType = destinationType;
            this.source = source;
            this.formatProvider = formatProvider;
            key = new ConverterKey(sourceType, destinationType);
        }

        public bool Equals(ConverterContext other)
        {
            return other != null
                && other.FormatProvider == FormatProvider
                && other.SourceType == SourceType
                && other.DestinationType == DestinationType
                && other.Source == Source
                && other.Key == Key;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConverterContext);
        }

        public override int GetHashCode()
        {
            int hash = 19;

            hash = (hash * 23) + SourceType.GetHashCode();
            hash = (hash * 23) + DestinationType.GetHashCode();
            hash = (hash * 23) + FormatProvider.GetHashCode();
            hash = (hash * 23) + Key.GetHashCode();

            if (Source != null) hash = (hash * 23) + FormatProvider.GetHashCode();

            return hash;
        }
    }
}
