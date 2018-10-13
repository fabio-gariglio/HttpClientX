using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Autofac.Tests.TestHandlers
{
    public class HttpMessageHandlerWithValueDependency : HttpMessageHandler
    {
        public HttpMessageHandlerWithValueDependency(string value)
        {
            Debug.Assert(value != null);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}