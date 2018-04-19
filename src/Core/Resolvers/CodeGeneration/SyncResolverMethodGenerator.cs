using System.Linq;
using System.Text;

namespace HotChocolate.Resolvers.CodeGeneration
{
    public sealed class SyncResolverMethodGenerator
        : SourceCodeGenerator
    {
        protected override void GenerateResolverInvocation(
            FieldResolverDescriptor resolverDescriptor, StringBuilder source)
        {
            source.AppendLine($"var resolver = ctx.{nameof(IResolverContext.Service)}<{resolverDescriptor.ResolverType.FullName}>();");
            source.AppendLine($"return Task.FromResult<object>(resolver.{resolverDescriptor.MemberName} (");

            if (resolverDescriptor.Arguments.Any())
            {
                string arguments = string.Join(", ", 
                    resolverDescriptor.Arguments.Select(t => t.Name));
                source.AppendLine(arguments);
            }

            source.Append("));");
        }

        public override bool CanGenerate(
            FieldResolverDescriptor resolverDescriptor)
                => !resolverDescriptor.IsAsync
                    && resolverDescriptor.IsMethod
                    && resolverDescriptor.Kind == FieldResolverKind.Collection;
    }
}