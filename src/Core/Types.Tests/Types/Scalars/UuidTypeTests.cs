﻿using System;
using HotChocolate.Language;
using Xunit;

namespace HotChocolate.Types
{
    public class UuidTypeTests
    {
        [Fact]
        public void IsInstanceOfType_StringLiteral()
        {
            // arrange
            var uuidType = new UuidType();
            var guid = Guid.NewGuid();
            var literal = new StringValueNode(guid.ToString("N"));

            // act
            bool isOfType = uuidType.IsInstanceOfType(guid);

            // assert
            Assert.True(isOfType);
        }

        [Fact]
        public void IsInstanceOfType_NullLiteral()
        {
            // arrange
            var uuidType = new UuidType();
            var guid = Guid.NewGuid();
            var literal = new NullValueNode(null);

            // act
            bool isOfType = uuidType.IsInstanceOfType(literal);

            // assert
            Assert.True(isOfType);
        }

        [Fact]
        public void IsInstanceOfType_IntLiteral()
        {
            // arrange
            var uuidType = new UuidType();
            var guid = Guid.NewGuid();
            var literal = new IntValueNode(123);

            // act
            bool isOfType = uuidType.IsInstanceOfType(literal);

            // assert
            Assert.False(isOfType);
        }

        [Fact]
        public void IsInstanceOfType_Null()
        {
            // arrange
            var uuidType = new UuidType();
            var guid = Guid.NewGuid();

            // act
            Action action = () => uuidType.IsInstanceOfType(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Serialize_Guid()
        {
            // arrange
            var uuidType = new UuidType();
            var guid = Guid.NewGuid();
            var expectedValue = guid.ToString("N");

            // act
            var serializedValue = uuidType.Serialize(guid);

            // assert
            Assert.Equal(guid, Assert.IsType<Guid>(serializedValue));
        }

        [Fact]
        public void Serialize_Null()
        {
            // arrange
            var uuidType = new UuidType();

            // act
            var serializedValue = uuidType.Serialize(null);

            // assert
            Assert.Null(serializedValue);
        }

        [Fact]
        public void ParseLiteral_StringValueNode()
        {
            // arrange
            var uuidType = new UuidType();
            var expected = Guid.NewGuid();
            var literal = new StringValueNode(expected.ToString());

            // act
            var actual = (Guid)uuidType
                .ParseLiteral(literal);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseLiteral_IntValueNode()
        {
            // arrange
            var uuidType = new UuidType();
            var literal = new IntValueNode(123);

            // act
            Action action = () => uuidType.ParseLiteral(literal);

            // assert
            Assert.Throws<ScalarSerializationException>(action);
        }

        [Fact]
        public void ParseLiteral_NullValueNode()
        {
            // arrange
            var uuidType = new UuidType();
            NullValueNode literal = NullValueNode.Default;

            // act
            var value = uuidType.ParseLiteral(literal);

            // assert
            Assert.Null(value);
        }

        [Fact]
        public void ParseLiteral_Null()
        {
            // arrange
            var uuidType = new UuidType();

            // act
            Action action = () => uuidType.ParseLiteral(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ParseValue_Guid()
        {
            // arrange
            var uuidType = new UuidType();
            var expected = Guid.NewGuid();
            var expectedLiteralValue = expected.ToString("N");

            // act
            var stringLiteral =
                (StringValueNode)uuidType.ParseValue(expected);

            // assert
            Assert.Equal(expectedLiteralValue, stringLiteral.Value);
        }

        [Fact]
        public void ParseValue_Null()
        {
            // arrange
            var uuidType = new UuidType();
            Guid? guid = null;

            // act
            IValueNode stringLiteral =
                uuidType.ParseValue(guid);

            // assert
            Assert.True(stringLiteral is NullValueNode);
            Assert.Null(((NullValueNode)stringLiteral).Value);
        }

        [Fact]
        public void EnsureDateTypeKindIsCorret()
        {
            // arrange
            var type = new UuidType();

            // assert
            Assert.Equal(TypeKind.Scalar, type.Kind);
        }
    }
}
