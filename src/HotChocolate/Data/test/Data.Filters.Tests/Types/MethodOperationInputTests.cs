﻿using System.Linq;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.Data.Filters
{
    public class MethodOperationInputTests
    {
        [Fact]
        public void Create_Explicit_Operation()
        {
            // arrange
            var convention = new FilterConvention(x => x.UseMock());

            // act
            ISchema schema = SchemaBuilder.New()
                .AddQueryType<QueryExplicit>()
                .AddConvention<IFilterConvention>(convention)
                .UseFiltering()
                .Create();

            // assert
            schema.ToString().MatchSnapshot();
        }

        [Fact]
        public void Create_Implicit_Operation()
        {
            // arrange
            var convention = new FilterConvention(x => x.UseMock());

            // act
            ISchema schema = SchemaBuilder.New()
                .AddQueryType<Query>()
                .AddConvention<IFilterConvention>(convention)
                .UseFiltering()
                .Create();

            // assert
            schema.ToString().MatchSnapshot();
        }

        public class QueryExplicit
        {
            [UseFiltering(Type = typeof(FooOperationType))]
            public IQueryable<Foo> Foos() => new Foo[0].AsQueryable();
        }

        public class Query
        {
            [UseFiltering]
            public IQueryable<Foo> Foos() => new Foo[0].AsQueryable();
        }

        public class FooOperationType : FilterInputType<Foo>
        {
            protected override void Configure(IFilterInputTypeDescriptor<Foo> descriptor)
            {
                descriptor.Name("Test");
                descriptor.Operation(x => x.SimpleMethod()).Name("TestSimpleMethod");
                descriptor.Operation(x => x.ComplexMethod()).Name("TestComplexMethod");
            }
        }

        public class Foo
        {
            public bool SimpleMethod() => true;

            public Bar ComplexMethod() => new Bar();
        }

        public class Bar
        {
            public string StringOperation { get; set; } = "";
        }
    }
}