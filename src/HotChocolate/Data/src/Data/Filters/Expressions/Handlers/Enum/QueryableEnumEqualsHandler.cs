using HotChocolate.Configuration;
using HotChocolate.Utilities;

namespace HotChocolate.Data.Filters.Expressions
{
    public class QueryableEnumEqualsHandler
        : QueryableComparableEqualsHandler
    {
        public QueryableEnumEqualsHandler(
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
