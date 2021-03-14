using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using HotChocolate.Language;

namespace HotChocolate.Types.Scalars
{
    /// <summary>
    /// The `LocalTime` scalar type represents a local time string in 24-hr HH:mm[:ss[.SSS]] format.
    /// </summary>
    public class LocalTimeType : ScalarType<DateTime, StringValueNode>
    {
        private const string _dateFormat = "HH:mm.ss";
        public LocalTimeType(
            NameString name,
            BindingBehavior bind = BindingBehavior.Explicit)
            : base(name, bind)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalTimeType"/> class.
        /// </summary>
        public LocalTimeType(
            NameString name,
            string? description = null,
            BindingBehavior bind = BindingBehavior.Explicit)
            : base(name, bind)
        {
            Description = description;
        }

        public override IValueNode ParseResult(object? resultValue)
        {
            throw new NotImplementedException();
        }

        protected override DateTime ParseLiteral(StringValueNode valueSyntax)
        {
            throw new NotImplementedException();
        }

        protected override StringValueNode ParseValue(DateTime runtimeValue)
        {
            throw new NotImplementedException();
        }
    }
}
