using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Language;

namespace HotChocolate.Stitching.Requests
{
    internal class MergeRequestRewriter : QuerySyntaxRewriter<bool>
    {
        private static readonly NameNode _defaultName = new NameNode("exec_batch");

        private readonly List<FieldNode> _fields = new List<FieldNode>();
        private readonly Dictionary<string, VariableDefinitionNode> _variables =
            new Dictionary<string, VariableDefinitionNode>();
        private readonly Dictionary<string, FragmentDefinitionNode> _fragments =
            new Dictionary<string, FragmentDefinitionNode>();

        private Dictionary<string, string>? _aliases;
        private NameString _requestPrefix;
        private bool _rewriteFragments;
        private OperationType? _operationType;
        private NameNode? _operationName;

        public void SetOperationName(NameNode name) => _operationName = name;

        public IDictionary<string, string> AddQuery(
            BufferedRequest request,
            NameString requestPrefix,
            bool isAutoGenerated)
        {
            _requestPrefix = requestPrefix;
            _rewriteFragments = !isAutoGenerated;
            _operationType = request.Operation.Operation;
            _aliases = new Dictionary<string, string>();

            DocumentNode rewritten =
                RewriteDocument(request.Document, true);

            OperationDefinitionNode operation =
                BufferedRequest.ResolveOperation(rewritten, request.Request.OperationName);

            foreach (VariableDefinitionNode variable in operation.VariableDefinitions)
            {
                if (!_variables.ContainsKey(variable.Variable.Name.Value))
                {
                    _variables.Add(variable.Variable.Name.Value, variable);
                }
            }

            _fields.AddRange(operation.SelectionSet.Selections.OfType<FieldNode>());

            foreach (FragmentDefinitionNode fragment in rewritten.Definitions
                .OfType<FragmentDefinitionNode>())
            {
                if (!_fragments.ContainsKey(fragment.Name.Value))
                {
                    _fragments.Add(fragment.Name.Value, fragment);
                }
            }

            return _aliases;
        }

        public DocumentNode Merge()
        {
            var definitions = new List<IDefinitionNode>
            {
                new OperationDefinitionNode
                (
                    null,
                    _operationName ?? _defaultName,
                    _operationType ?? OperationType.Query,
                    new List<VariableDefinitionNode>(_variables.Values),
                    Array.Empty<DirectiveNode>(),
                    new SelectionSetNode(null, new List<ISelectionNode>(_fields))
                )
            };

            definitions.AddRange(_fragments.Values);

            return new DocumentNode(null, definitions);
        }

        protected override VariableDefinitionNode RewriteVariableDefinition(
            VariableDefinitionNode node, bool context) =>
            node.WithVariable(node.Variable.WithName(
                node.Variable.Name.CreateNewName(_requestPrefix)));

        protected override FieldNode RewriteField(FieldNode node, bool first)
        {
            FieldNode current = node;

            if (first)
            {
                NameNode responseName = node.Alias ?? node.Name;
                NameNode alias = responseName.CreateNewName(_requestPrefix);
                _aliases![alias.Value] = responseName.Value;
                current = current.WithAlias(alias);
            }

            current = Rewrite(current, node.Arguments, first,
                (p, c) => RewriteMany(p, c, RewriteArgument),
                current.WithArguments);

            if (node.SelectionSet != null)
            {
                current = Rewrite(current, node.SelectionSet, false,
                    RewriteSelectionSet, current.WithSelectionSet);
            }

            return current;
        }

        protected override FragmentSpreadNode RewriteFragmentSpread(
            FragmentSpreadNode node, bool first) =>
            _rewriteFragments
                ? node.WithName(node.Name.CreateNewName(_requestPrefix))
                : node;

        protected override FragmentDefinitionNode RewriteFragmentDefinition(
            FragmentDefinitionNode node, bool first) =>
            _rewriteFragments
                ? base.RewriteFragmentDefinition(
                    node.WithName(node.Name.CreateNewName(_requestPrefix)),
                    false)
                : base.RewriteFragmentDefinition(node, false);

        protected override VariableNode RewriteVariable(
            VariableNode node, bool first) =>
            node.WithName(node.Name.CreateNewName(_requestPrefix));
    }
}
