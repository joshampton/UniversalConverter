using System;

namespace UniversalConverter
{
    public sealed class ConverterKey
    {
        private readonly Type sourceType;
        private readonly Type destinationType;

        public Type SourceType { get { return this.sourceType; } }
        public Type DestinationType { get { return this.destinationType; } }

        public ConverterKey(Type sourceType, Type destinationType)
        {
            if (sourceType == null)
                throw new ArgumentNullException("sourceType");

            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

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
            var target = obj as ConverterKey;

            return target != null
                && target.SourceType == this.SourceType
                && target.DestinationType == this.DestinationType;
        }

        public static bool operator ==(ConverterKey left, ConverterKey right)
        {
            return (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                || (!object.ReferenceEquals(left, null) && left.Equals(right));
        }

        public static bool operator !=(ConverterKey left, ConverterKey right)
        {
            return !(left == right);
        }
    }
}
