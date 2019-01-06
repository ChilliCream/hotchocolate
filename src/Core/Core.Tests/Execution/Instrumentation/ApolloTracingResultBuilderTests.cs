using System;
using ChilliCream.Testing;
using Xunit;

namespace HotChocolate.Execution.Instrumentation
{
    public class ApolloTracingResultBuilderTests
    {
        [Fact]
        public void BuildEmptyTracingResult()
        {
            // arrange
            var builder = new ApolloTracingResultBuilder();

            // act
            ApolloTracingResult result = builder.Build();

            // assert
            result.Snapshot();
        }

        [Fact]
        public void BuildTracingResult()
        {
            // arrange
            var builder = new ApolloTracingResultBuilder();
            DateTimeOffset startTime = new DateTime(
                636824022698524527,
                DateTimeKind.Utc);
            const long startTimestamp = 1113752384890500;
            var rosolverStatisticsA = new ResolverStatistics
            {
                Path = new IPathSegment[]
                {
                    new FieldNamePathSegment("root"),
                    new FieldNamePathSegment("field"),
                    new ListIndexPathSegment(0),
                    new FieldNamePathSegment("value")
                },
                ParentType = "ParentTypeA",
                FieldName = "FieldNameA",
                StartTimestamp = 1113752444890200,
                EndTimestamp = 1113752454811100
            };
            var rosolverStatisticsB = new ResolverStatistics
            {
                Path = new IPathSegment[]
                {
                    new FieldNamePathSegment("root"),
                    new FieldNamePathSegment("field"),
                    new ListIndexPathSegment(1),
                    new FieldNamePathSegment("value")
                },
                ParentType = "ParentTypeB",
                FieldName = "FieldNameB",
                StartTimestamp = 1113752464890200,
                EndTimestamp = 1113752484850000
            };
            TimeSpan duration = TimeSpan.FromMilliseconds(122);

            builder.SetRequestStartTime(startTime, startTimestamp);
            builder.SetParsingResult(1113752394890300, 1113752402820700);
            builder.SetValidationResult(1113752404890400, 1113752434898800);
            builder.AddResolverResult(rosolverStatisticsA);
            builder.AddResolverResult(rosolverStatisticsB);
            builder.SetRequestDuration(duration);

            // act
            ApolloTracingResult result = builder.Build();

            // assert
            result.Snapshot();
        }
    }
}
