using System;
using HotChocolate.Internal;
using Xunit;

namespace HotChocolate.Types.Descriptors
{
    public class ClrTypeReferenceTests
    {
        [InlineData(typeof(string[]), TypeContext.Input, "foo")]
        [InlineData(typeof(string[]), TypeContext.Input, null)]
        [InlineData(typeof(string), TypeContext.Input, null)]
        [InlineData(typeof(string[]), TypeContext.Output, null)]
        [InlineData(typeof(string), TypeContext.None, null)]
        [Theory]
        public void TypeReference_Create(
            Type clrType,
            TypeContext context,
            string scope)
        {
            // arrange
            // act
            ClrTypeReference typeReference = TypeReference.Create(
                clrType,
                context,
                scope: scope);

            // assert
            Assert.Equal(clrType, typeReference.Type.OriginalType);
            Assert.Equal(context, typeReference.Context);
            Assert.Equal(scope, typeReference.Scope);
        }

        [Fact]
        public void TypeReference_Create_Generic()
        {
            // arrange
            // act
            ClrTypeReference typeReference = TypeReference.Create<int>(
                TypeContext.Input,
                scope: "abc");

            // assert
            Assert.Equal(typeof(int), typeReference.Type.OriginalType);
            Assert.Equal(TypeContext.Input, typeReference.Context);
            Assert.Equal("abc", typeReference.Scope);
        }

        [Fact]
        public void TypeReference_Create_And_Infer_Output_Context()
        {
            // arrange
            // act
            ClrTypeReference typeReference = TypeReference.Create(
                typeof(ObjectType<string>),
                scope: "abc");

            // assert
            Assert.Equal(typeof(ObjectType<string>), typeReference.Type.OriginalType);
            Assert.Equal(TypeContext.Output, typeReference.Context);
            Assert.Equal("abc", typeReference.Scope);
        }

        [Fact]
        public void TypeReference_Create_And_Infer_Input_Context()
        {
            // arrange
            // act
            ClrTypeReference typeReference = TypeReference.Create(
                typeof(InputObjectType<string>),
                scope: "abc");

            // assert
            Assert.Equal(typeof(InputObjectType<string>), typeReference.Type.OriginalType);
            Assert.Equal(TypeContext.Input, typeReference.Context);
            Assert.Equal("abc", typeReference.Scope);
        }

        [Fact]
        public void TypeReference_Create_Generic_And_Infer_Output_Context()
        {
            // arrange
            // act
            ClrTypeReference typeReference =
                TypeReference.Create<ObjectType<string>>(scope: "abc");

            // assert
            Assert.Equal(typeof(ObjectType<string>), typeReference.Type.OriginalType);
            Assert.Equal(TypeContext.Output, typeReference.Context);
            Assert.Equal("abc", typeReference.Scope);
        }

        [Fact]
        public void TypeReference_Create_Generic_And_Infer_Input_Context()
        {
            // arrange
            // act
            ClrTypeReference typeReference =
                TypeReference.Create<InputObjectType<string>>(scope: "abc");

            // assert
            Assert.Equal(typeof(InputObjectType<string>), typeReference.Type.OriginalType);
            Assert.Equal(TypeContext.Input, typeReference.Context);
            Assert.Equal("abc", typeReference.Scope);
        }

        [Fact]
        public void ClrTypeReference_Equals_To_Null()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var result = x.Equals((ClrTypeReference)null);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void ClrTypeReference_Equals_To_Same()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var xx = x.Equals((ClrTypeReference)x);

            // assert
            Assert.True(xx);
        }

        [Fact]
        public void ClrTypeReference_Equals_Context_None_Does_Not_Matter()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output);

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals(y);
            var xz = x.Equals(z);
            var yz = y.Equals(z);

            // assert
            Assert.True(xy);
            Assert.True(xz);
            Assert.False(yz);
        }

        [Fact]
        public void ClrTypeReference_Equals_Scope_Different()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None,
                scope: "a");

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output,
                scope: "a");

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals(y);
            var xz = x.Equals(z);
            var yz = y.Equals(z);

            // assert
            Assert.True(xy);
            Assert.False(xz);
            Assert.False(yz);
        }

        [Fact]
        public void ITypeReference_Equals_To_Null()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var result = x.Equals((ITypeReference)null);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void ITypeReference_Equals_To_Same()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var xx = x.Equals((ITypeReference)x);

            // assert
            Assert.True(xx);
        }

        [Fact]
        public void ITypeReference_Equals_To_SyntaxTypeRef()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var xx = x.Equals(TypeReference.Create(new NameType("foo")));

            // assert
            Assert.False(xx);
        }

        [Fact]
        public void ITypeReference_Equals_Context_None_Does_Not_Matter()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output);

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals((ITypeReference)y);
            var xz = x.Equals((ITypeReference)z);
            var yz = y.Equals((ITypeReference)z);

            // assert
            Assert.True(xy);
            Assert.True(xz);
            Assert.False(yz);
        }

        [Fact]
        public void ITypeReference_Equals_Scope_Different()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None,
                scope: "a");

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output,
                scope: "a");

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals((ITypeReference)y);
            var xz = x.Equals((ITypeReference)z);
            var yz = y.Equals((ITypeReference)z);

            // assert
            Assert.True(xy);
            Assert.False(xz);
            Assert.False(yz);
        }

        [Fact]
        public void Object_Equals_To_Null()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var result = x.Equals((object)null);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Object_Equals_To_Same()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var xx = x.Equals((object)x);

            // assert
            Assert.True(xx);
        }

        [Fact]
        public void Object_Equals_To_Object()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            // act
            var xx = x.Equals(new object());

            // assert
            Assert.False(xx);
        }

        [Fact]
        public void Object_Equals_Context_None_Does_Not_Matter()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output);

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals((object)y);
            var xz = x.Equals((object)z);
            var yz = y.Equals((object)z);

            // assert
            Assert.True(xy);
            Assert.True(xz);
            Assert.False(yz);
        }

        [Fact]
        public void Object_Equals_Scope_Different()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None,
                scope: "a");

            var y = TypeReference.Create(
                typeof(string),
                TypeContext.Output,
                scope: "a");

            var z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xy = x.Equals((object)y);
            var xz = x.Equals((object)z);
            var yz = y.Equals((object)z);

            // assert
            Assert.True(xy);
            Assert.False(xz);
            Assert.False(yz);
        }

        [Fact]
        public void ClrTypeReference_ToString()
        {
            // arrange
            ClrTypeReference typeReference = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var result = typeReference.ToString();

            // assert
            Assert.Equal("Input: System.String", result);
        }

        [Fact]
        public void ClrTypeReference_WithType()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.WithType(typeof(int));

            // assert
            Assert.Equal(typeof(int), typeReference2.Type.OriginalType);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_WithType_Null()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            Action action = () => typeReference1.WithType(default(IExtendedType)!);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ClrTypeReference_WithType_Null2()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            Action action = () => typeReference1.WithType(default(Type)!);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ClrTypeReference_WithContext()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.WithContext(TypeContext.Output);

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(TypeContext.Output, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_WithContext_Nothing()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.WithContext();

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(TypeContext.None, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_WithScope()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.WithScope("bar");

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Equal("bar", typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_WithScope_Nothing()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.WithScope();

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Null(typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_With()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.With(
                typeof(int).ToExtendedType(),
                TypeContext.Output,
                scope: "bar");

            // assert
            Assert.Equal(typeof(int), typeReference2.Type.OriginalType);
            Assert.Equal(TypeContext.Output, typeReference2.Context);
            Assert.Equal("bar", typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_With_Nothing()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.With();

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_With_Type()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.With(typeof(int).ToExtendedType());

            // assert
            Assert.Equal(typeof(int), typeReference2.Type.OriginalType);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_With_Context()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.With(context: TypeContext.None);

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(TypeContext.None, typeReference2.Context);
            Assert.Equal(typeReference1.Scope, typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_With_Scope()
        {
            // arrange
            ClrTypeReference typeReference1 = TypeReference.Create(
                typeof(string),
                TypeContext.Input,
                scope: "foo");

            // act
            ClrTypeReference typeReference2 = typeReference1.With(scope: "bar");

            // assert
            Assert.Equal(typeReference1.Type, typeReference2.Type);
            Assert.Equal(typeReference1.Context, typeReference2.Context);
            Assert.Equal("bar", typeReference2.Scope);
        }

        [Fact]
        public void ClrTypeReference_GetHashCode()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None,
                scope: "foo");

            ClrTypeReference y = TypeReference.Create(
                typeof(string),
                TypeContext.None,
                scope: "foo");

            ClrTypeReference z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xh = x.GetHashCode();
            var yh = y.GetHashCode();
            var zh = z.GetHashCode();

            // assert
            Assert.Equal(xh, yh);
            Assert.NotEqual(xh, zh);
        }

        [Fact]
        public void ClrTypeReference_GetHashCode_Context_HasNoEffect()
        {
            // arrange
            ClrTypeReference x = TypeReference.Create(
                typeof(string),
                TypeContext.None);

            ClrTypeReference y = TypeReference.Create(
                typeof(string),
                TypeContext.Output);

            ClrTypeReference z = TypeReference.Create(
                typeof(string),
                TypeContext.Input);

            // act
            var xh = x.GetHashCode();
            var yh = y.GetHashCode();
            var zh = z.GetHashCode();

            // assert
            Assert.Equal(xh, yh);
            Assert.Equal(xh, zh);
        }
    }
}
