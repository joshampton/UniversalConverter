using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConverter
{
    public sealed class ConverterContext : IEquatable<ConverterContext>
    {
        private readonly IFormatProvider formatProvider;
        private readonly Type sourceType;
        private readonly Type destinationType;
        private readonly object source;
        private readonly ConverterKey key;

        public Type SourceType { get { return this.sourceType; } }
        public Type DestinationType { get { return this.destinationType; } }
        public IFormatProvider FormatProvider { get { return this.formatProvider; } }
        public object Source { get { return this.source; } }
        public ConverterKey Key { get { return this.key; } }

        public ConverterContext(Type sourceType, Type destinationType, object source, IFormatProvider formatProvider)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");

            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if (formatProvider == null)
                throw new ArgumentNullException("provider");

            this.sourceType = sourceType;
            this.destinationType = destinationType;
            this.source = source;
            this.formatProvider = formatProvider;
            this.key = new ConverterKey(sourceType, destinationType);
        }

        public bool Equals(ConverterContext other)
        {
            return other != null
                && other.FormatProvider == this.FormatProvider
                && other.SourceType == this.SourceType
                && other.DestinationType == this.DestinationType
                && other.Source == this.Source
                && other.Key == this.Key;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConverterContext);
        }

        public override int GetHashCode()
        {
            int hash = 19;

            hash = (hash * 23) + this.SourceType.GetHashCode();
            hash = (hash * 23) + this.DestinationType.GetHashCode();
            hash = (hash * 23) + this.FormatProvider.GetHashCode();
            hash = (hash * 23) + this.Key.GetHashCode();

            if (Source != null)
                hash = (hash * 23) + this.FormatProvider.GetHashCode();

            return hash;
        }
    }
}
