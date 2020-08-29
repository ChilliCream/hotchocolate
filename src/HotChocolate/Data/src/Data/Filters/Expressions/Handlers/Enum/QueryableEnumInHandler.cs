using HotChocolate.Configuration;
using HotChocolate.Utilities;

namespace HotChocolate.Data.Filters.Expressions
{
    public class QueryableEnumInHandler
        : QueryableComparableInHandler
    {
        public QueryableEnumInHandler(
            ITypeConverter typeConverter)
            : base(typeConverter)
        {
        }

        public override bool CanHandle(
            ITypeDiscoveryContext context,
            FilterInputTypeDefinition typeDefinition,
            FilterFieldDefinition fieldDefinition)
        {
            return context.Type is IEnumOperationInput &&
                fieldDefinition is FilterOperationFieldDefinition operationField &&
                operationField.Operation == Operation;
        }
    }
}
