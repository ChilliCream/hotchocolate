using System.Text;
using HotChocolate.Utilities;

namespace HotChocolate.Resolvers.CodeGeneration
{
    internal sealed class SyncOnBeforeInvokeMethodGenerator
        : OnBeforeInvokeSourceCodeGenerator<DirectiveMiddlewareDescriptor>
    {
        protected override void GenerateResolverInvocation(
            DirectiveMiddlewareDescriptor resolverDescriptor,
            StringBuilder source)
        {
            source.AppendLine($"var resolver = ctx.{nameof(IResolverContext.Resolver)}<{resolverDescriptor.Type.GetTypeName()}>();");
            HandleExceptionsAsync(source, s =>
            {
                s.Append($"resolver.{resolverDescriptor.Method.Name}(");
                GenerateArguments(resolverDescriptor, s);
                s.AppendLine(");");
                s.Append("return System.Threading.Tasks.Task.CompletedTask;");
            });
        }

        protected override bool CanHandle(
            DirectiveMiddlewareDescriptor descriptor)
        {
            return descriptor.Kind == MiddlewareKind.OnBeforeInvoke
                && !descriptor.IsAsync;
        }
    }
}
