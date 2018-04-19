using System.Text;

namespace HotChocolate.Resolvers.CodeGeneration
{
    public abstract class SourceCodeGenerator
        {
            public string Generate(
                string resolverName,
                FieldResolverDescriptor resolverDescriptor)
            {
                StringBuilder source = new StringBuilder();
                source.Append("public static Func<IResolverContext, CancellationToken, Task<object>");
                source.Append(" ");
                source.Append(resolverName);
                source.Append(" ");
                source.Append(" = ");
                if (resolverDescriptor.IsAsync)
                {
                    source.Append("async ");
                }
                source.Append("(ctx, ct) => {");
                source.AppendLine();

                foreach (FieldResolverArgumentDescriptor argumentDescriptor in resolverDescriptor.Arguments)
                {
                    GenerateArgumentInvocation(argumentDescriptor, source);
                    source.AppendLine();
                }

                GenerateResolverInvocation(resolverDescriptor, source);

                source.AppendLine();
                source.Append("}");
                return source.ToString();
            }

            private void GenerateArgumentInvocation(
                FieldResolverArgumentDescriptor argumentDescriptor,
                StringBuilder source)
            {
                source.Append($"var {argumentDescriptor.Name} = ");
                switch (argumentDescriptor.Kind)
                {
                    case FieldResolverArgumentKind.Argument:
                        source.Append($"ctx.{nameof(IResolverContext.Argument)}<{argumentDescriptor.Type.FullName}>({argumentDescriptor.Name})");
                        break;
                    case FieldResolverArgumentKind.Field:
                        source.Append($"ctx.{nameof(IResolverContext.Field)}");
                        break;
                    case FieldResolverArgumentKind.FieldSelection:
                        source.Append($"ctx.{nameof(IResolverContext.FieldSelection)}");
                        break;
                    case FieldResolverArgumentKind.ObjectType:
                        source.Append($"ctx.{nameof(IResolverContext.ObjectType)}");
                        break;
                    case FieldResolverArgumentKind.OperationDefinition:
                        source.Append($"ctx.{nameof(IResolverContext.OperationDefinition)}");
                        break;
                    case FieldResolverArgumentKind.QueryDocument:
                        source.Append($"ctx.{nameof(IResolverContext.QueryDocument)}");
                        break;
                    case FieldResolverArgumentKind.Schema:
                        source.Append($"ctx.{nameof(IResolverContext.Schema)}");
                        break;
                    case FieldResolverArgumentKind.Service:
                        source.Append($"ctx.{nameof(IResolverContext.Service)}<{argumentDescriptor.Type.FullName}>()");
                        break;
                    case FieldResolverArgumentKind.Source:
                        source.Append($"ctx.{nameof(IResolverContext.Parent)}<{argumentDescriptor.Type.FullName}>()");
                        break;
                }
                source.Append(";");
            }

            protected abstract void GenerateResolverInvocation(
                FieldResolverDescriptor resolverDescriptor,
                StringBuilder source);

            public abstract bool CanGenerate(
                FieldResolverDescriptor resolverDescriptor);
        }
}