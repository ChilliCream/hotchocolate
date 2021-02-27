using System.IO;
using ChilliCream.Testing;
using Xunit;
using static StrawberryShake.CodeGeneration.CSharp.GeneratorTestHelper;

namespace StrawberryShake.CodeGeneration.CSharp
{
    public class ScalarGeneratorTests
    {
        [Fact]
        public void Simple_Custom_Scalar() =>
            AssertResult(
                "query GetPerson { person { name email } }",
                "type Query { person: Person }",
                "type Person { name: String! email: Email }",
                "scalar Email",
                "extend schema @key(fields: \"id\")");

        [Fact]
        public void Only_Custom_Scalars() =>
            AssertResult(
                "query GetPerson { person { email } }",
                "type Query { person: Person }",
                "type Person { email: Email }",
                "scalar Email",
                "extend schema @key(fields: \"id\")");

        [Fact]
        public void Custom_Scalar_With_RuntimeType() =>
            AssertResult(
                "query GetPerson { person { name email } }",
                "type Query { person: Person }",
                "type Person { name: String! email: Email }",
                "scalar Email",
                "extend scalar Email @runtimeType(name: \"global::System.Int32\")",
                "extend schema @key(fields: \"id\")");

        [Fact]
        public void Custom_Scalar_With_SerializationType() =>
            AssertResult(
                "query GetPerson { person { name email } }",
                "type Query { person: Person }",
                "type Person { name: String! email: Email }",
                "scalar Email",
                "extend scalar Email @serializationType(name: \"global::System.Int32\")",
                "extend schema @key(fields: \"id\")");

        [Fact]
        public void Custom_Scalar_With_SerializationType_And_RuntimeType() =>
            AssertResult(
                "query GetPerson { person { name email } }",
                "type Query { person: Person }",
                "type Person { name: String! email: Email }",
                "scalar Email",
                @"extend scalar Email 
                    @runtimeType(name: ""global::System.Int64"")
                    @serializationType(name: ""global::System.Int32"")",
                "extend schema @key(fields: \"id\")");

        [Fact]
        public void Complete_Schema_With_Uuid_And_DateTime()
        {
            AssertResult(
                FileResource.Open("AllExpenses.graphql"),
                FileResource.Open("Expenses.extensions.graphql"),
                FileResource.Open("Expenses.graphql"));  
        }
    }
}
