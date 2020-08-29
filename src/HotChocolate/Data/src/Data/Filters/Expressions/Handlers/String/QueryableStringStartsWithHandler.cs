using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    public class QueryableStringStartsWithHandler : QueryableStringOperationHandler
    {
        public QueryableStringStartsWithHandler()
        {
            CanBeNull = false;
        }

        protected override int Operation => DefaultOperations.StartsWith;

        public override Expression HandleOperation(
            QueryableFilterContext context,
            IFilterInputType declaringType,
            IFilterOperationField field,
            IType fieldType,
            IValueNode value,
            object parsedValue)
        {
            Expression property = context.GetInstance();
            return FilterExpressionBuilder.StartsWith(property, parsedValue);
        }
    }
}
