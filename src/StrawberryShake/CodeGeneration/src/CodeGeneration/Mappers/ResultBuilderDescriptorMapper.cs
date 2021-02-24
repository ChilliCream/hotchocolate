using System.Linq;
using StrawberryShake.CodeGeneration.Analyzers.Models;
using static StrawberryShake.CodeGeneration.NamingConventions;

namespace StrawberryShake.CodeGeneration.Mappers
{
    public class ResultBuilderDescriptorMapper
    {
        public static void Map(ClientModel model, IMapperContext context)
        {
            foreach (OperationModel modelOperation in model.Operations)
            {
                var resultTypeName =
                    CreateResultRootTypeName(modelOperation.ResultType.Name);

                context.Register(
                    modelOperation.Name,
                    new ResultBuilderDescriptor(
                        modelOperation.Name,
                        context.Types.Single(t => t.RuntimeType.Name.Equals(resultTypeName)),
                        modelOperation.LeafTypes.Select(
                            leafType => new ValueParserDescriptor(
                                leafType.Name,
                                TypeInfos.GetOrCreate(leafType.RuntimeType),
                                TypeInfos.GetOrCreate(leafType.SerializationType))).ToList()));
            }
        }
    }
}
