using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Handlers
{
    public class DefaultRequestHeadersHandler : DelegatingHandler
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public DefaultRequestHeadersHandler(HttpMessageHandler innerHandler, string headerName, string headerValue) : base(innerHandler)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.TryAddWithoutValidation(_headerName, _headerValue);
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}