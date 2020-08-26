using System;
using System.Collections.Generic;
using System.Reflection;
using HotChocolate.Internal;
using HotChocolate.Types;
using Xunit;

#nullable enable

namespace HotChocolate.Internal
{
    public class ExtendedTypeRewriterTests
    {
        private readonly TypeCache _typeCache = new TypeCache();

        [InlineData("Array", "[Byte!]!")]
        [InlineData("NullableArray", "[Byte!]")]
        [InlineData("ArrayNullableElement", "[Byte]!")]
        [InlineData("NullableArrayNullableElement", "[Byte]")]
        [InlineData("ObjectArray", "[Object!]!")]
        [InlineData("NullableObjectArray", "[Object!]")]
        [InlineData("ObjectArrayNullableElement", "[Object]!")]
        [InlineData("NullableObjectArrayNullableElement", "[Object]")]
        [Theory]
        public void DetectNullabilityOnArrays(string methodName, string typeName)
        {
            // arrange
            MethodInfo method = typeof(Arrays).GetMethod(methodName)!;

            // act
            ExtendedType extendedType = ExtendedType.FromMember(method, _typeCache);

            // assert
            Assert.Equal(typeName, extendedType.ToString());
        }

        [InlineData("List", "List<Byte!>!")]
        [InlineData("NullableList", "List<Byte!>")]
        [InlineData("ListNullableElement", "List<Byte>!")]
        [InlineData("NullableListNullableElement", "List<Byte>")]
        [InlineData("ObjectList", "List<Object!>!")]
        [InlineData("NullableObjectList", "List<Object!>")]
        [InlineData("ObjectListNullableElement", "List<Object>!")]
        [InlineData("NullableObjectListNullableElement", "List<Object>")]
        [Theory]
        public void DetectNullabilityOnLists(string methodName, string typeName)
        {
            // arrange
            MethodInfo method = typeof(Lists).GetMethod(methodName)!;

            // act
            ExtendedType extendedType = ExtendedType.FromMember(method, _typeCache);

            // assert
            Assert.Equal(typeName, extendedType.ToString());
        }

        public class Arrays
        {
            public byte[] Array()
            {
                throw new NotImplementedException();
            }

            public byte[]? NullableArray()
            {
                throw new NotImplementedException();
            }

            public byte?[] ArrayNullableElement()
            {
                throw new NotImplementedException();
            }

            public byte?[]? NullableArrayNullableElement()
            {
                throw new NotImplementedException();
            }

            public object[] ObjectArray()
            {
                throw new NotImplementedException();
            }

            public object[]? NullableObjectArray()
            {
                throw new NotImplementedException();
            }

            public object?[] ObjectArrayNullableElement()
            {
                throw new NotImplementedException();
            }

            public object?[]? NullableObjectArrayNullableElement()
            {
                throw new NotImplementedException();
            }
        }

        public class Lists
        {
            public List<byte> List()
            {
                throw new NotImplementedException();
            }

            public List<byte>? NullableList()
            {
                throw new NotImplementedException();
            }

            public List<byte?> ListNullableElement()
            {
                throw new NotImplementedException();
            }

            public List<byte?>? NullableListNullableElement()
            {
                throw new NotImplementedException();
            }

            public List<object> ObjectList()
            {
                throw new NotImplementedException();
            }

            public List<object>? NullableObjectList()
            {
                throw new NotImplementedException();
            }

            public List<object?> ObjectListNullableElement()
            {
                throw new NotImplementedException();
            }

            public List<object?>? NullableObjectListNullableElement()
            {
                throw new NotImplementedException();
            }
        }
    }
}
