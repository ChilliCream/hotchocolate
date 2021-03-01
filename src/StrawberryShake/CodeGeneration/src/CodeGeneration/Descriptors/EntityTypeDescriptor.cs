using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using static StrawberryShake.CodeGeneration.NamingConventions;

namespace StrawberryShake.CodeGeneration
{
    public class EntityTypeDescriptor : ICodeDescriptor
    {
        /// <summary>
        /// Create a new instance of <see cref="EntityTypeDescriptor" />
        /// </summary>
        /// <param name="name">
        /// The name of the GraphQL type
        /// </param>
        /// <param name="namespace">
        /// The namespace of the runtime type
        /// </param>
        /// <param name="operationTypes">
        /// The operation types of this entity
        /// </param>
        /// <param name="documentation">
        /// The documentation of this entity
        /// </param>
        public EntityTypeDescriptor(
            NameString name,
            string @namespace,
            IReadOnlyList<ComplexTypeDescriptor> operationTypes,
            string? documentation)
        {
            var allProperties = new Dictionary<string, PropertyDescriptor>();

            foreach (PropertyDescriptor namedTypeReferenceDescriptor in
                operationTypes.SelectMany(operationType => operationType.Properties))
            {
                if (!allProperties.ContainsKey(namedTypeReferenceDescriptor.Name))
                {
                    allProperties.Add(
                        namedTypeReferenceDescriptor.Name,
                        namedTypeReferenceDescriptor);
                }
            }

            Properties = allProperties;
            RuntimeType = new(CreateEntityTypeName(name), @namespace);
            Name = name;
            Documentation = documentation;
        }

        /// <summary>
        /// Gets the GraphQL type name which this entity represents.
        /// </summary>
        public NameString Name { get; }

        /// <summary>
        /// Gets the entity name.
        /// </summary>
        public RuntimeTypeInfo RuntimeType { get; }

        /// <summary>
        /// The documentation of this type
        /// </summary>
        public string? Documentation { get; }

        /// <summary>
        /// Gets the properties of this entity.
        /// </summary>
        public Dictionary<string, PropertyDescriptor> Properties { get; }
    }
}
