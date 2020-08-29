using System;
using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate.Data.Filters.Expressions
{
    public class QueryableListSomeOperationHandler : QueryableListOperationHandlerBase
    {
        protected override int Operation => DefaultOperations.Some;

        protected override Expression HandleListOperation(
            QueryableFilterContext context,
            IFilterInputType declaringType,
            IFilterField field,
            IType fieldType,
            ObjectFieldNode node,
            Type closureType,
            LambdaExpression lambda) =>
            FilterExpressionBuilder.Any(closureType, context.GetInstance(), lambda);
    }
}
