using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Execution;
using HotChocolate.Utilities;
using HotChocolate.Language;

namespace HotChocolate.PersistedQueries.FileSystem
{
    /// <summary>
    /// An implementation of <see cref="IReadStoredQueries"/> that
    /// uses the local file system as a storage medium.
    /// </summary>
    public class FileSystemStorage
        : IReadStoredQueries
        , IWriteStoredQueries
    {
        private readonly IQueryFileMap _queryMap;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="queryMap">The query identifier mapping.</param>
        public FileSystemStorage(IQueryFileMap queryMap)
        {
            _queryMap = queryMap
                ?? throw new ArgumentNullException(nameof(queryMap));
        }

        /// <inheritdoc />
        public Task<IQuery> TryReadQueryAsync(string queryId) =>
            TryReadQueryAsync(queryId, CancellationToken.None);

        /// <inheritdoc />
        public async Task<IQuery> TryReadQueryAsync(
            string queryId,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(queryId))
            {
                throw new ArgumentNullException(nameof(queryId));
            }

            var filePath = _queryMap.MapToFilePath(queryId);

            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read))
            {
                DocumentNode document = await BufferHelper.ReadAsync(
                    stream,
                    (buffer, buffered) =>
                    {
                        var span = buffer.AsSpan().Slice(0, buffered);
                        return Utf8GraphQLParser.Parse(span);
                    },
                    cancellationToken);
                return new QueryDocument(document);
            }
        }

        /// <inheritdoc />
        public Task WriteQueryAsync(string queryId, IQuery query) =>
            WriteQueryAsync(queryId, query, CancellationToken.None);

        /// <inheritdoc />
        public async Task WriteQueryAsync(
            string queryId,
            IQuery query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(queryId))
            {
                throw new ArgumentNullException(nameof(queryId));
            }

            var filePath = _queryMap.MapToFilePath(queryId);

            if (!File.Exists(filePath))
            {
                using (var stream = new FileStream(
                    filePath, FileMode.CreateNew, FileAccess.Write))
                {
                    await query.WriteToAsync(stream, cancellationToken);
                }
            }
        }
    }
}
