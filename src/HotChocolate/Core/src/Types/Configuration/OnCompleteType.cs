using System.Collections.Generic;
using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Definitions;

#nullable enable

namespace Microsoft.Extensions.DependencyInjection
{
    public delegate void OnCompleteType(
        ICompletionContext context,
        DefinitionBase? definition,
        IDictionary<string, object?> contextData);
}