using System.Collections.Generic;
using System.Linq;

namespace HotChocolate.Resolvers
{
    public static class FieldResolverDescriptorExtensions
    {
        public static int ArgumentCount(this FieldResolverDescriptor descriptor)
        {
            return descriptor.ArgumentDescriptors
                .Count(t => t.Kind == ArgumentKind.Argument);
        }

        public static IEnumerable<ArgumentDescriptor> Arguments(
            this FieldResolverDescriptor descriptor)
        {
            return descriptor.ArgumentDescriptors
                .Where(t => t.Kind == ArgumentKind.Argument);
        }
    }
}
