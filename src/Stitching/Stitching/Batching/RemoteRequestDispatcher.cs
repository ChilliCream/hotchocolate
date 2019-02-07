using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Execution;
using HotChocolate.Language;

namespace HotChocolate.Stitching
{
    internal class RemoteRequestDispatcher
    {
        private readonly IServiceProvider _services;
        private readonly IQueryExecutor _queryExecutor;

        public RemoteRequestDispatcher(
            IServiceProvider services,
            IQueryExecutor queryExecutor)
        {
            _services = services
                ?? throw new ArgumentNullException(nameof(services));
            _queryExecutor = queryExecutor
                ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public Task DispatchAsync(
            IList<BufferedRequest> requests,
            CancellationToken cancellationToken)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            if (requests.Count == 0)
            {
                return Task.CompletedTask;
            }

            var rewriter = new MergeQueryRewriter();
            var variableValues = new Dictionary<string, object>();

            for (int i = 0; i < requests.Count; i++)
            {
                MergeRequest(requests[i], rewriter, variableValues, $"__{i}_");
            }

            return DispatchRequestsAsync(
                requests,
                rewriter.Merge(),
                variableValues,
                cancellationToken);
        }

        private async Task DispatchRequestsAsync(
            IList<BufferedRequest> requests,
            DocumentNode mergedQuery,
            IReadOnlyDictionary<string, object> variableValues,
            CancellationToken cancellationToken)
        {
            var mergedRequest = new QueryRequest(
                QuerySyntaxSerializer.Serialize(mergedQuery))
            {
                VariableValues = variableValues,
                Services = _services
            };

            var mergedResult = (IReadOnlyQueryResult)await _queryExecutor
                .ExecuteAsync(mergedRequest, cancellationToken);
            var handledErrors = new HashSet<IError>();

            for (int i = 0; i < requests.Count; i++)
            {
                IQueryResult result = ExtractResult(
                    requests[i].Aliases,
                    mergedResult,
                    handledErrors);

                if (handledErrors.Count < mergedResult.Errors.Count
                    && i == requests.Count - 1)
                {
                    foreach (IError error in mergedResult.Errors
                        .Except(handledErrors))
                    {
                        result.Errors.Add(error);
                    }
                }

                requests[i].Promise.SetResult(result);
            }
        }

        private void MergeRequest(
            BufferedRequest bufferedRequest,
            MergeQueryRewriter rewriter,
            IDictionary<string, object> variableValues,
            NameString requestPrefix)
        {
            MergeVariables(
                bufferedRequest.Request.VariableValues,
                variableValues,
                requestPrefix);

            bufferedRequest.Aliases = rewriter.AddQuery(
                bufferedRequest.Document, requestPrefix);
        }

        private void MergeVariables(
            IReadOnlyDictionary<string, object> original,
            IDictionary<string, object> merged,
            NameString requestPrefix)
        {
            foreach (KeyValuePair<string, object> item in original)
            {
                string variableName = MergeUtils.CreateNewName(
                    item.Key, requestPrefix);
                merged.Add(variableName, item.Value);
            }
        }

        private IQueryResult ExtractResult(
            IDictionary<string, string> aliases,
            IReadOnlyQueryResult mergedResult,
            ICollection<IError> handledErrors)
        {
            var result = new QueryResult();

            foreach (KeyValuePair<string, string> alias in aliases)
            {
                if (mergedResult.Data.TryGetValue(alias.Key, out object o))
                {
                    result.Data.Add(alias.Value, o);
                }
            }

            foreach (IError error in mergedResult.Errors)
            {
                if (error.Path != null
                    && error.Path.FirstOrDefault() is string s
                    && aliases.ContainsKey(s))
                {
                    handledErrors.Add(error);
                    result.Errors.Add(error);
                }
            }

            return result;
        }
    }
}
