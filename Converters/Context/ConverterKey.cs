using System;

namespace UniversalConverter
{
    public sealed class ConverterKey : IEquatable<ConverterKey>
    {
        private readonly Type sourceType;
        private readonly Type destinationType;

        public Type SourceType { get { return sourceType; } }
        public Type DestinationType { get { return destinationType; } }

        public ConverterKey(Type sourceType, Type destinationType)
        {
            if (sourceType == null) throw new ArgumentNullException("sourceType");

            if (destinationType == null) throw new ArgumentNullException("destinationType");

            this.sourceType = sourceType;
            this.destinationType = destinationType;
        }

        public override int GetHashCode()
        {
            int hash = 19;

            hash = (hash * 17) + SourceType.GetHashCode();
            hash = (hash * 17) + DestinationType.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConverterKey);
        }


        public bool Equals(ConverterKey other)
        {
            return other != null
                && other.SourceType == SourceType
                && other.DestinationType == DestinationType;
        }

        public static bool operator ==(ConverterKey left, ConverterKey right)
        {
            return (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                || (!ReferenceEquals(left, null) && left.Equals(right));
        }

        public static bool operator !=(ConverterKey left, ConverterKey right)
        {
            return !(left == right);
        }

    }
}
