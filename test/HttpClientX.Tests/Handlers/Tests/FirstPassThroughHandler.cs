using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Tests.Handlers.Tests
{
    public class FirstPassThroughHandler : DelegatingHandler
    {
        public static DateTime ExecutedDateTime;
        public static HttpRequestMessage ReceivedRequest;
        
        public FirstPassThroughHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ExecutedDateTime = DateTime.UtcNow;
            ReceivedRequest = request;
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}