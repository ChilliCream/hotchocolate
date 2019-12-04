using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using StrawberryShake.Transport;

namespace StrawberryShake.Http
{
    public class HttpOperationContext
        : IHttpOperationContext
        , IDisposable
    {
        private Dictionary<string, object>? _contextData;
        private bool _disposed;

        public HttpOperationContext(
            IOperation operation,
            IOperationResultBuilder result,
            IResultParser resultParser,
            HttpClient client,
            CancellationToken requestAborted)
        {
            Operation = operation
                ?? throw new ArgumentNullException(nameof(operation));
            Result = result
                ?? throw new ArgumentNullException(nameof(result));
            ResultParser = resultParser
                ?? throw new ArgumentNullException(nameof(resultParser));
            Client = client
                ?? throw new ArgumentNullException(nameof(client));
            RequestAborted = requestAborted;
            MessageWriter = new MessageWriter();
        }

        public IOperation Operation { get; }

        public IOperationResultBuilder Result { get; }

        public IResultParser ResultParser { get; }

        public IDictionary<string, object> ContextData
        {
            get
            {
                return _contextData ??= new Dictionary<string, object>();
            }
        }

        public CancellationToken RequestAborted { get; }

        public HttpRequestMessage? HttpRequest { get; set; }

        public HttpResponseMessage? HttpResponse { get; set; }

        public IRequestWriter MessageWriter { get; }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                MessageWriter.Dispose();
                _disposed = true;
            }
        }
    }
}
