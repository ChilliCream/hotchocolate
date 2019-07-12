using System;
using System.Linq.Expressions;
using HotChocolate.Language;
using HotChocolate.Utilities;

namespace HotChocolate.Types.Filters.Expressions
{
    public class ComparableLowerThanOperationHandler
        : IExpressionOperationHandler
    {
        public bool TryHandle(
            FilterOperation operation,
            IInputType type,
            IValueNode value,
            Expression instance,
            ITypeConversion converter,
            out Expression expression)
        {
            if (operation.Type == typeof(IComparable)
                && (value is IntValueNode
                    || value is FloatValueNode
                    || value is EnumValueNode
                    || value.IsNull()))
            {
                MemberExpression property =
                    Expression.Property(instance, operation.Property);
                var parsedValue = type.ParseLiteral(value);

                if (operation.Property.PropertyType
                    .IsInstanceOfType(parsedValue))
                {
                    parsedValue = converter.Convert(
                        typeof(object),
                        operation.Property.PropertyType,
                        parsedValue);
                }

                switch (operation.Kind)
                {

                    case FilterOperationKind.LowerThan:
                        expression = FilterExpressionBuilder.LowerThan(
                            property, parsedValue);
                        return true;

                    case FilterOperationKind.NotLowerThan:
                        expression = FilterExpressionBuilder.Not(
                            FilterExpressionBuilder.LowerThan(
                                property, parsedValue));
                        return true;
                }
            }

            expression = null;
            return false;
        }
    }
}
