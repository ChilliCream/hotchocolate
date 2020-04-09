﻿using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HotChocolate.Validation
{
    public class RequiredArgumentRuleTests
        : DocumentValidatorVisitorTestBase
    {
        public RequiredArgumentRuleTests()
            : base(builder => builder.AddArgumentRules())
        {
        }

        [Fact]
        public void BooleanArgFieldAndNonNullBooleanArgField()
        {
            ExpectValid(@"
                query {
                    arguments {
                        ... goodBooleanArg
                        ... goodNonNullArg
                    }
                }

                fragment goodBooleanArg on Arguments {
                    booleanArgField(booleanArg: true)
                }

                fragment goodNonNullArg on Arguments {
                    nonNullBooleanArgField(nonNullBooleanArg: true)
                }
            ");
        }

        [Fact]
        public void GoodBooleanArgDefault()
        {
            ExpectValid(@"
                query {
                    arguments {
                        ... goodBooleanArgDefault
                    }
                }

                fragment goodBooleanArgDefault on Arguments {
                    booleanArgField
                }
            ");
        }

        [Fact]
        public void MissingRequiredArg()
        {
            // arrange
            ExpectErrors(@"
                query {
                    arguments {
                        ... missingRequiredArg
                    }
                }

                fragment missingRequiredArg on Arguments {
                    nonNullBooleanArgField
                }
            ",
            t => Assert.Equal(
                $"The argument `nonNullBooleanArg` is required.", t.Message));
        }

        [Fact]
        public void MissingRequiredArgNonNullBooleanArg()
        {
            ExpectErrors(@"
                query {
                    arguments {
                        ... missingRequiredArg
                    }
                }

                fragment missingRequiredArg on Arguments {
                    nonNullBooleanArgField(nonNullBooleanArg: null)
                }
            ",
            t => Assert.Equal(
                $"The argument `nonNullBooleanArg` is required.", t.Message));
        }

        [Fact]
        public void MissingRequiredDirectiveArg()
        {
            ExpectErrors(@"
                query {
                    arguments {
                        ... missingRequiredArg
                    }
                }

                fragment missingRequiredArg on Arguments {
                    nonNullBooleanArgField(nonNullBooleanArg: true) @skip()
                }
            ",
            t => Assert.Equal(
                $"The argument `if` is required.", t.Message));
        }

        [Fact]
        public void IgnoresUnknownArguments()
        {
            ExpectValid(@"
                {
                    dog {
                        isHouseTrained(unknownArgument: true)
                    }
                }
            ");
        }

        [Fact]
        public void ArgOnOptionalArg()
        {
            ExpectValid(@"
              {
                dog {
                        isHouseTrained(atOtherHomes: true)
                    }
                }
            ");
        }

        [Fact]
        public void ArgOnNoArgOnOptionalArg()
        {
            ExpectValid(@"
                {
                    dog {
                        isHouseTrained
                    }
                }
            ");
        }

        [Fact]
        public void NoArgOnNonNullFieldWithDefault()
        {
            ExpectValid(@"
              {
                arguments {
                        nonNullFieldWithDefault
                    }
                }
            ");
        }

        [Fact]
        public void MultipleArgs()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleReqs(x: 1, y: 2)
                    }
                }
            ");
        }

        [Fact]
        public void MultipleArgsReverseOrder()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleReqs(req2: 2, req1: 1)
                    }
                }
            ");
        }

        [Fact]
        public void NoArgsOnMultipleOptional()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOpts
                    }
                }
            ");
        }

        [Fact]
        public void OneArgOnMultipleOptional()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOpts(opt1: 1)
                    }
                }
            ");
        }

        [Fact]
        public void SecondArgOnMultipleOptional()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOpts(opt2: 2)
                    }
                }
            ");
        }

        [Fact]
        public void MultipleRequiredArgsOnMixedList()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOptsAndReqs(req1: 3, req2: 4)
                    }
                }
            ");
        }

        [Fact]
        public void MultipleRequiredAndOneOptionalArgOnMixedList()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOptsAndReqs(req1: 3, req2: 4, opt1: 5)
                    }
                }
            ");
        }

        [Fact]
        public void AllRequiredAndOptionalArgsOnMixedList()
        {
            ExpectValid(@"
                {
                    arguments {
                        multipleOptsAndReqs(req1: 3, req2: 4, opt1: 5, opt2: 6)
                    }
                }
            ");
        }

        [Fact]
        public void MissingOneNonNullableArgument()
        {
            ExpectErrors(@"
                {
                    arguments {
                        multipleReqs(req2: 2)
                    }
                }
            ");
        }

        [Fact]
        public void MissingMultipleNonNullableArguments()
        {
            ExpectErrors(@"
                {
                    arguments {
                        multipleReqs
                    }
                }
            ");
        }

        [Fact]
        public void IncorrectValueAndMissingArgument()
        {
            ExpectErrors(@"
                {
                    arguments {
                        multipleReqs(req1: ""one"")
                    }
                }
            ");
        }

        [Fact]
        public void IgnoresUnknownDirectives()
        {
            ExpectValid(@"
                {
                    dog @unknown
                }
            ");
        }

        [Fact]
        public void WithDirectivesOfValidTypes()
        {
            ExpectValid(@"
                {
                    dog @include(if: true) {
                        name
                    }
                    human @skip(if: false) {
                        name
                    }
                }
            ");
        }

        [Fact]
        public void WithDirectiveWithMissingTypes()
        {
            ExpectErrors(@"
               {
                    dog @include {
                        name @skip
                    }
                }
            ");
        }
    }
}
