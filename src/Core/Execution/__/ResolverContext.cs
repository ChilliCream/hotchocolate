using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace HotChocolate.Execution
{
    // TODO : maybe move to resolvers
    internal class ResolverContext
        : IResolverContext
    {
        private static readonly ArgumentResolver _argumentResolver = new ArgumentResolver();
        private readonly ExecutionContext _executionContext;
        private readonly FieldResolverTask _fieldResolverTask;
        private Dictionary<string, object> _arguments;

        public ResolverContext(ExecutionContext executionContext, FieldResolverTask fieldResolverTask)
        {
            if (executionContext == null)
            {
                throw new ArgumentNullException(nameof(executionContext));
            }

            if (fieldResolverTask == null)
            {
                throw new ArgumentNullException(nameof(fieldResolverTask));
            }

            _executionContext = executionContext;
            _fieldResolverTask = fieldResolverTask;
            _arguments = _argumentResolver.CoerceArgumentValues(
                fieldResolverTask.ObjectType, fieldResolverTask.Field,
                executionContext.Variables);
        }

        public Schema Schema => _executionContext.Schema;

        public ObjectType ObjectType => _fieldResolverTask.ObjectType;

        public Field Field => _fieldResolverTask.Field.Field;

        public DocumentNode QueryDocument => _executionContext.QueryDocument;

        public OperationDefinitionNode Operation => _executionContext.Operation;

        public FieldNode FieldSelection => _fieldResolverTask.Field.Node;

        public ImmutableStack<object> Path => _fieldResolverTask.Source;

        public T Argument<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!_arguments.TryGetValue(name, out object argumentValue))
            {
                return default(T);
            }

            return (T)argumentValue;
        }

        public T Parent<T>()
        {
            return (T)Path.Peek();
        }

        public T Service<T>()
        {
            return (T)_executionContext.GetService(typeof(T));
        }
    }
}
