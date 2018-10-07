using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Tests.Handlers
{
    public class StubHandler : HttpMessageHandler
    {
        public static HttpRequestMessage ReceivedRequest;

        // TODO: It shouldn't be necessary to define a constructor with a HttpMessageHandler parameter
        public StubHandler(HttpMessageHandler innerHandler)
        {
            
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ReceivedRequest = request;
            
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            
            return Task.FromResult(response);
        }
    }
}