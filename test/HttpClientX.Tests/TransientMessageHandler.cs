using System.Net.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Tests
{
    public class TransientMessageHandler : DelegatingHandler
    {
        private readonly string _value;

        public TransientMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _value = Guid.NewGuid().ToString();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            response.Headers.Add("transient-header", _value);

            return response;
        }
    }
}
