﻿using System.Collections.Generic;
using System.Linq;
using HotChocolate.Configuration;
using Xunit;

namespace HotChocolate.Types.Relay
{
    public class EdgeTypeTests
        : TypeTestBase
    {
        [Fact]
        public void CheckThatNameIsCorrect()
        {
            // arrange
            // act
            var type = new EdgeType<StringType>();

            // assert
            Assert.Equal("StringEdge", type.Name);
        }

        [Fact]
        public void CheckFieldsAreCorrect()
        {
            // arrange
            // act
            var type = CreateType(new EdgeType<StringType>());

            // assert
            Assert.Collection(type.Fields.Where(t => !t.IsIntrospectionField),
                t =>
                {
                    Assert.Equal("cursor", t.Name);
                    Assert.IsType<NonNullType>(t.Type);
                    Assert.IsType<StringType>(((NonNullType)t.Type).Type);
                },
                t =>
                {
                    Assert.Equal("node", t.Name);
                    Assert.IsType<StringType>(t.Type);
                });
        }
    }
}
