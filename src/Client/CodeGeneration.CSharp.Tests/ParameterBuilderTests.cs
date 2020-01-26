using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snapshooter;
using Snapshooter.Xunit;
using Xunit;

namespace StrawberryShake.CodeGeneration.CSharp
{
    public class ParameterBuilderTests
    {
        [Fact]
        public async Task CreateParameter()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ParameterBuilder.New()
                .SetName("foo")
                .SetType("Bar")
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task CreateParameter_With_Default()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ParameterBuilder.New()
                .SetName("foo")
                .SetType("Bar")
                .SetDefault()
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task CreateParameter_With_Custom_Default()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ParameterBuilder.New()
                .SetName("foo")
                .SetType("Bar")
                .SetDefault("1")
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }
    }

    public class ConstructorBuilderTests
    {
        [Fact]
        public async Task CreateConstructor()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ConstructorBuilder.New()
                .SetTypeName("GetAbc")
                .AddCode(CodeLineBuilder.New().SetLine("return;"))
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task CreateConstructor_With_One_Parameter()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ConstructorBuilder.New()
                .SetTypeName("GetAbc")
                .AddCode(CodeLineBuilder.New().SetLine("return;"))
                .AddParameter(ParameterBuilder.New().SetName("abc").SetType("String"))
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task CreateConstructor_With_Two_Parameter()
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ConstructorBuilder.New()
                .SetTypeName("GetAbc")
                .AddCode(CodeLineBuilder.New().SetLine("return;"))
                .AddParameter(ParameterBuilder.New().SetName("abc").SetType("String"))
                .AddParameter(ParameterBuilder.New().SetName("def").SetType("String").SetDefault())
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot();
        }

        [InlineData(AccessModifier.Public)]
        [InlineData(AccessModifier.Internal)]
        [InlineData(AccessModifier.Protected)]
        [InlineData(AccessModifier.Private)]
        [Theory]
        public async Task CreateConstructor_With_AccessModifier(AccessModifier accessModifier)
        {
            // arrange
            var sb = new StringBuilder();
            var writer = new CodeWriter(sb);

            // act
            await ConstructorBuilder.New()
                .SetTypeName("GetAbc")
                .SetAccessModifier(accessModifier)
                .AddCode(CodeLineBuilder.New().SetLine("return;"))
                .BuildAsync(writer);

            // assert
            sb.ToString().MatchSnapshot(
                new SnapshotNameExtension(
                    accessModifier.ToString()));
        }
    }
}
