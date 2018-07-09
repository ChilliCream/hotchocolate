using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Language;
using HotChocolate.Validation;

namespace HotChocolate.Execution
{
    public partial class QueryExecuter
    {
        private readonly ISchema _schema;
        private readonly QueryValidator _queryValidator;
        private readonly Cache<QueryInfo> _queryCache;
        private readonly Cache<OperationRequest> _operationCache;
        private readonly bool _useCache;

        public QueryExecuter(ISchema schema)
            : this(schema, 100)
        {
        }

        public QueryExecuter(ISchema schema, int cacheSize)
        {
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _queryValidator = new QueryValidator(schema);
            _queryCache = new Cache<QueryInfo>(cacheSize);
            _operationCache = new Cache<OperationRequest>(cacheSize * 10);
            _useCache = cacheSize > 0;
            CacheSize = cacheSize;
        }

        public int CacheSize { get; }

        public int CachedQueries => _queryCache.Usage;

        public int CachedOperations => _queryCache.Usage;

        public async Task<IExecutionResult> ExecuteAsync(
            QueryRequest queryRequest,
            CancellationToken cancellationToken = default)
        {
            if (queryRequest == null)
            {
                throw new ArgumentNullException(nameof(queryRequest));
            }

            QueryInfo queryInfo = GetOrCreateQuery(queryRequest.Query);
            if (queryInfo.ValidationResult.HasErrors)
            {
                return new QueryResult(queryInfo.ValidationResult.Errors);
            }

            try
            {
                OperationRequest operationRequest =
                    GetOrCreateOperationRequest(
                        queryRequest, queryInfo.QueryDocument);

                return await operationRequest.ExecuteAsync(
                    queryRequest.VariableValues, queryRequest.InitialValue,
                    cancellationToken);
            }
            catch (QueryException ex)
            {
                return new QueryResult(ex.Errors);
            }
            catch (Exception ex)
            {
                return new QueryResult(CreateErrorFromException(ex));
            }
        }

        private IQueryError CreateErrorFromException(Exception exception)
        {
            if (_schema.Options.DeveloperMode)
            {
                return new QueryError(
                    $"{exception.Message}\r\n\r\n{exception.StackTrace}");
            }
            else
            {
                return new QueryError("Unexpected execution error.");
            }
        }
    }
}
