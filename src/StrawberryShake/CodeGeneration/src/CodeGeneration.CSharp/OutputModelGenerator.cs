using System;
using System.Threading.Tasks;
using StrawberryShake.CodeGeneration.CSharp.Builders;

namespace StrawberryShake.CodeGeneration.CSharp
{
    public class OutputModelGenerator
        : CodeGenerator<OutputModelDescriptor>
    {
        protected override Task WriteAsync(
            CodeWriter writer,
            OutputModelDescriptor descriptor)
        {
            if (writer is null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            ClassBuilder builder =
                ClassBuilder.New()
                    .SetAccessModifier(AccessModifier.Public)
                    .SetName(descriptor.Name);

            foreach(var typeName in descriptor.Implements)
            {
                builder.AddImplements(typeName);
            }

            foreach (OutputFieldDescriptor field in descriptor.Fields)
            {
                builder.AddProperty(
                    PropertyBuilder.New()
                        .SetAccessModifier(AccessModifier.Public)
                        .SetName(field.Name)
                        .SetType(field.Type));
            }

            return builder.BuildAsync(writer);
        }
    }
}
