using HotChocolate.Language;
using HotChocolate.Resolvers;

namespace HotChocolate.Types
{
    public interface IDirective
    {
        string Name { get; }

        DirectiveType Type { get; }

        DirectiveResolver Resolver { get; }

        bool IsExecutable { get; }

        T ToObject<T>();

        DirectiveNode ToNode();

        T GetArgument<T>(string argumentName);
    }
}
