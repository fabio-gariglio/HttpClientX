using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Tests
{
    public class SetResponseHeaderMessageHandler : DelegatingHandler
    {
        public string HeaderName { get; }
        public string HeaderValue { get; }

        public SetResponseHeaderMessageHandler(HttpMessageHandler innerHandler, string headerName, string headerValue) : base(innerHandler)
        {
            HeaderName = headerName;
            HeaderValue = headerValue;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            response.Headers.Add(HeaderName, HeaderValue);

            return response;
        }
    }
}
