using HotChocolate.Language;

namespace HotChocolate.Types
{
    /// <summary>
    /// The GraphQL Upload scalar.
    /// </summary>
    public class UploadType : ScalarType<IFile, FileValueNode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadType"/> class.
        /// </summary>
        public UploadType()
            : this("Upload")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadType"/> class.
        /// </summary>
        public UploadType(
            NameString name,
            string? description = null,
            BindingBehavior bind = BindingBehavior.Explicit)
            : base(name, bind)
        {
            Description = description;
        }

        public override IValueNode ParseResult(object? resultValue)
        {
            if (resultValue is null)
            {
                return NullValueNode.Default;
            }

            if (resultValue is IFile file)
            {
                return new FileValueNode(file);
            }

            throw base.CreateParseValueError(resultValue);
        }

        protected override IFile ParseLiteral(FileValueNode valueSyntax) =>
            valueSyntax.Value;

        protected override FileValueNode ParseValue(IFile runtimeValue) =>
            new FileValueNode(runtimeValue);

        public override bool TrySerialize(object? runtimeValue, out object? resultValue)
        {
            throw new GraphQLException(
                "The GraphQL upload scalar is only valid to use as an input type.");
        }

        public override bool TryDeserialize(object? resultValue, out object? runtimeValue)
        {
            if (resultValue is null)
            {
                runtimeValue = null;
                return true;
            }

            if (resultValue is IFile file)
            {
                runtimeValue = file;
                return true;
            }

            runtimeValue = null;
            return false;
        }
    }
}
