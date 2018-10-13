using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Autofac.Tests.TestHandlers
{
    public interface IService
    {
    }

    public class ServiceImplementation : IService
    {
    }

    public class HttpMessageHandlerWithHandlerAndServiceDependency : HttpMessageHandler
    {
        public HttpMessageHandlerWithHandlerAndServiceDependency(HttpMessageHandler handler, IService service)
        {
            Debug.Assert(handler != null);
            Debug.Assert(service != null);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}