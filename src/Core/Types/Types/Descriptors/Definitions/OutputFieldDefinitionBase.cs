﻿using System.Collections.Generic;
using HotChocolate.Language;

namespace HotChocolate.Types.Descriptors.Definitions
{
    public class OutputFieldDefinitionBase
        : FieldDefinitionBase<FieldDefinitionNode>
        , ICanBeDeprecated
    {
        public string DeprecationReason { get; set; }

        public bool IsDeprecated => !string.IsNullOrEmpty(DeprecationReason);

        public ICollection<ArgumentDefinition> Arguments { get; } =
            new List<ArgumentDefinition>();

        public IEnumerable<ITypeReference> GetDependencies()
        {
            yield return Type;

            foreach (ArgumentDefinition argument in Arguments)
            {
                yield return argument.Type;
            }
        }
    }
}
