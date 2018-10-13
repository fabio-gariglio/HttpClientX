using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Autofac.Tests.TestHandlers
{
    public class HttpMessageHandlerWithNoDependencies : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}