﻿using System.Collections.Generic;
using HotChocolate.Language.Utilities;

namespace HotChocolate.Language
{
    public sealed class EnumTypeDefinitionNode
        : EnumTypeDefinitionNodeBase
        , ITypeDefinitionNode
    {
        public EnumTypeDefinitionNode(
            Location? location,
            NameNode name,
            StringValueNode? description,
            IReadOnlyList<DirectiveNode> directives,
            IReadOnlyList<EnumValueDefinitionNode> values)
            : base(location, name, directives, values)
        {
            Description = description;
        }

        public override NodeKind Kind { get; } = NodeKind.EnumTypeDefinition;

        public StringValueNode? Description { get; }

        public override IEnumerable<ISyntaxNode> GetNodes()
        {
            if (Description is { })
            {
                yield return Description;
            }

            yield return Name;

            foreach (DirectiveNode directive in Directives)
            {
                yield return directive;
            }

            foreach (EnumValueDefinitionNode value in Values)
            {
                yield return value;
            }
        }

        public override string ToString() => SyntaxPrinter.Print(this, true);

        public override string ToString(bool indented) => SyntaxPrinter.Print(this, indented);

        public EnumTypeDefinitionNode WithLocation(Location? location)
        {
            return new EnumTypeDefinitionNode(
                location, Name, Description,
                Directives, Values);
        }

        public EnumTypeDefinitionNode WithName(NameNode name)
        {
            return new EnumTypeDefinitionNode(
                Location, name, Description,
                Directives, Values);
        }

        public EnumTypeDefinitionNode WithDescription(
            StringValueNode? description)
        {
            return new EnumTypeDefinitionNode(
                Location, Name, description,
                Directives, Values);
        }

        public EnumTypeDefinitionNode WithDirectives(
            IReadOnlyList<DirectiveNode> directives)
        {
            return new EnumTypeDefinitionNode(
                Location, Name, Description,
                directives, Values);
        }

        public EnumTypeDefinitionNode WithValues(
            IReadOnlyList<EnumValueDefinitionNode> values)
        {
            return new EnumTypeDefinitionNode(
                Location, Name, Description,
                Directives, values);
        }
    }
}
