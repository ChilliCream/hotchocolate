using System;

namespace HotChocolate.Types.Descriptors
{
    public sealed class ClrTypeReference
        : TypeReferenceBase
        , IClrTypeReference
    {
        public ClrTypeReference(
            Type type, TypeContext context)
            : this(type, context, null, null)
        {
        }

        public ClrTypeReference(
            Type type, TypeContext context,
            bool? isTypeNullable, bool? isElementTypeNullable)
            : base(context, isTypeNullable, isElementTypeNullable)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type = type;
        }

        public Type Type { get; }
    }
}
