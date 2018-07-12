using System;
using HotChocolate.Language;
using Xunit;

namespace HotChocolate.Validation
{
    public class QueryValidatorTests
    {
        [Fact]
        public void SchemaIsNulll()
        {
            // act
            Action a = () => new QueryValidator(null);

            // assert
            Assert.Throws<ArgumentNullException>(a);
        }

        [Fact]
        public void QueryIsNull()
        {
            // arrange
            Schema schema = ValidationUtils.CreateSchema();
            var queryValidator = new QueryValidator(schema);

            // act
            // act
            Action a = () => queryValidator.Validate(null);

            // assert
            Assert.Throws<ArgumentNullException>(a);
        }

        [Fact]
        public void QueryWithTypeSystemDefinitions()
        {
            // arrange
            DocumentNode query = Parser.Default.Parse(@"
                query getDogName {
                    dog {
                        name
                        color
                    }
                }

                extend type Dog {
                    color: String
                }
            ");

            Schema schema = ValidationUtils.CreateSchema();
            var queryValidator = new QueryValidator(schema);

            // act
            QueryValidationResult result = queryValidator.Validate(query);

            // assert
            Assert.True(result.HasErrors);
            Assert.Collection(result.Errors,
                t => Assert.Equal(
                    "A document containing TypeSystemDefinition " +
                    "is invalid for execution.", t.Message));
        }

        [Fact]
        public void QueryWithOneAnonymousAndOneNamedOperation()
        {
            // arrange
            DocumentNode query = Parser.Default.Parse(@"
                {
                    dog {
                        name
                    }
                }

                query getName {
                    dog {
                        owner {
                            name
                        }
                    }
                }
            ");

            Schema schema = ValidationUtils.CreateSchema();
            var queryValidator = new QueryValidator(schema);

            // act
            QueryValidationResult result = queryValidator.Validate(query);

            // assert
            Assert.True(result.HasErrors);
            Assert.Collection(result.Errors,
                t =>
                {
                    Assert.Equal(
                        "GraphQL allows a short‐hand form for defining query " +
                        "operations when only that one operation exists in the " +
                        "document.", t.Message);
                });
        }

        [Fact]
        public void TwoQueryOperationsWithTheSameName()
        {
            // arrange
            DocumentNode query = Parser.Default.Parse(@"
                query getName {
                    dog {
                        name
                    }
                }

                query getName {
                    dog {
                        owner {
                            name
                        }
                    }
                }
            ");

            Schema schema = ValidationUtils.CreateSchema();
            var queryValidator = new QueryValidator(schema);

            // act
            QueryValidationResult result = queryValidator.Validate(query);

            // assert
            Assert.True(result.HasErrors);
            Assert.Collection(result.Errors,
                t => Assert.Equal(
                        $"The operation name `getName` is not unique.",
                        t.Message));
        }

        [Fact]
        public void OperationWithTwoVariablesThatHaveTheSameName()
        {
            // arrange
            DocumentNode query = Parser.Default.Parse(@"
                query houseTrainedQuery($atOtherHomes: Boolean, $atOtherHomes: Boolean) {
                    dog {
                        isHousetrained(atOtherHomes: $atOtherHomes)
                    }
                }
            ");

            Schema schema = ValidationUtils.CreateSchema();
            var queryValidator = new QueryValidator(schema);

            // act
            QueryValidationResult result = queryValidator.Validate(query);

            // assert
            Assert.True(result.HasErrors);
            Assert.Collection(result.Errors,
                t => Assert.Equal(
                    "A document containing operations that " +
                    "define more than one variable with the same " +
                    "name is invalid for execution.", t.Message));
        }
    }
}
