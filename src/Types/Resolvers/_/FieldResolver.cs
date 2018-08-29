using System;

namespace HotChocolate.Resolvers
{
    public sealed class FieldResolver
        : FieldReferenceBase
        , IEquatable<FieldResolver>
    {
        public FieldResolver(
            string typeName, string fieldName,
            FieldResolverDelegate resolver)
            : base(typeName, fieldName)
        {
            Resolver = resolver
                ?? throw new ArgumentNullException(nameof(resolver));
        }

        public FieldResolverDelegate Resolver { get; }

        public FieldResolver WithTypeName(string typeName)
        {
            if (string.Equals(TypeName, typeName))
            {
                return this;
            }

            return new FieldResolver(typeName, FieldName, Resolver);
        }

        public FieldResolver WithFieldName(string fieldName)
        {
            if (string.Equals(FieldName, fieldName))
            {
                return this;
            }

            return new FieldResolver(TypeName, fieldName, Resolver);
        }

        public FieldResolver WithResolver(FieldResolverDelegate resolver)
        {
            if (string.Equals(Resolver, resolver))
            {
                return this;
            }

            return new FieldResolver(TypeName, FieldName, resolver);
        }

        public bool Equals(FieldResolver other)
        {
            return IsEqualTo(other);
        }

        public override bool Equals(object obj)
        {
            return IsReferenceEqualTo(obj)
                || IsEqualTo(obj as FieldResolver);
        }

        private bool IsEqualTo(FieldResolver other)
        {
            return base.IsEqualTo(other)
                && other.Resolver.Equals(Resolver);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397)
                    ^ (Resolver.GetHashCode() * 17);
            }
        }

        public override string ToString()
        {
            return $"{TypeName}.{FieldName}";
        }
    }
}
