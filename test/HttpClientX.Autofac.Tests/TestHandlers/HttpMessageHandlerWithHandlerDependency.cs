using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Autofac.Tests.TestHandlers
{
    public class HttpMessageHandlerWithHandlerDependency : HttpMessageHandler
    {
        public HttpMessageHandlerWithHandlerDependency(HttpMessageHandler handler)
        {
            Debug.Assert(handler != null);
            Debug.Assert(handler.GetType() == typeof(HttpClientHandler));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}