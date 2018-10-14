using System;
using System.Diagnostics;
using System.Net;
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
        public static HttpMessageHandler Handler;
        public static IService Service;
        public static HttpRequestMessage ReceivedRequest;

        public HttpMessageHandlerWithHandlerAndServiceDependency(HttpMessageHandler handler, IService service)
        {
            Debug.Assert(handler != null);
            Debug.Assert(service != null);
            
            Handler = handler;
            Service = service;
            ReceivedRequest = null;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ReceivedRequest = request;
            
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}