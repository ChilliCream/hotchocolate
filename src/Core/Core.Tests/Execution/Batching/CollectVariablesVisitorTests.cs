using System.Linq;
using HotChocolate.Language;
using HotChocolate.StarWars;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.Execution.Batching
{
    public class CollectVariablesVisitorTests
    {
        [Fact]
        public void FindUndeclaredVariables()
        {
            // arrange
            ISchema schema = SchemaBuilder.New()
                .AddStarWarsTypes()
                .Create();

            DocumentNode document = Utf8GraphQLParser.Parse(
                @"
                {
                    hero(episode: $ep) {
                        name
                    }
                }");

            OperationDefinitionNode operation = document.Definitions
                .OfType<OperationDefinitionNode>()
                .First();

            var visitor = new CollectVariablesVisitor(schema);
            var visitationMap = new CollectVariablesVisitationMap();

            // act
            operation.Accept(visitor, visitationMap);

            // assert
            var variables = operation.VariableDefinitions.ToList();
            variables.AddRange(visitor.GetVariableDeclarations());
            operation = operation.WithVariableDefinitions(variables);

            QuerySyntaxSerializer.Serialize(
                new DocumentNode(
                    new IDefinitionNode[]
                    {
                        operation
                    })).MatchSnapshot();
        }
    }
}
