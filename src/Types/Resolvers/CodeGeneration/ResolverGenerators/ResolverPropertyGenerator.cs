using System.Text;

namespace HotChocolate.Resolvers.CodeGeneration
{
    internal sealed class ResolverPropertyGenerator
        : ResolverSourceCodeGenerator<ResolverDescriptor>
    {
        protected override void GenerateResolverInvocation(
            ResolverDescriptor resolverDescriptor,
            StringBuilder source)
        {
            source.AppendLine($"var resolver = ctx.{nameof(IResolverContext.Resolver)}<{GetTypeName(resolverDescriptor.ResolverType)}>();");
            HandleExceptions(source, s =>
            {
                s.Append($"return resolver.{resolverDescriptor.Field.Member.Name};");
            });
        }

        protected override bool CanHandle(ResolverDescriptor descriptor)
        {
            return descriptor.IsProperty;
        }
    }
}
