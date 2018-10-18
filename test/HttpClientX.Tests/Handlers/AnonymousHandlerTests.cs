using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpClientX.Handlers;
using HttpClientX.Tests.Handlers.Tests;
using NUnit.Framework;

namespace HttpClientX.Tests.Handlers
{
    [TestFixture]
    public class AnonymousHandlerTests
    {
        [Test]
        public async Task It_should_be_possible_to_define_an_anonymous_message_handler()
        {
            var httpClient = new HttpClientBuilder()
                .Use((handler, request, token) =>
                {
                    request.Properties["intercepted"] = true;
                    
                    return handler(request, token);
                })
                .Use<StubHandler>()
                .Build();


            await httpClient.GetAsync("http://www.test.com");
            
            Assert.That(StubHandler.ReceivedRequest.Properties["intercepted"], Is.True);
        }
        
        [Test]
        public async Task It_should_be_possible_to_define_an_anonymous_message_handler_that_stops_the_handler_chain()
        {
            var httpClient = new HttpClientBuilder()
                .Use((handler, request, token) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted)))
                .Use<StubHandler>()
                .Build();

            var result = await httpClient.GetAsync("http://www.test.com");
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(StubHandler.ReceivedRequest, Is.Null);
        }
    }
}