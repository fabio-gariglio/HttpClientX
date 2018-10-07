using System.Threading.Tasks;
using HttpClientX.Handlers;
using NUnit.Framework;

namespace HttpClientX.Tests.Handlers
{
    [TestFixture]
    public class DefaultRequestHeadersHandlerTests
    {
        [Test]
        public async Task It_should_be_possible_to_set_a_request_header()
        {
            var httpClient = new HttpClientBuilder()
                .UseDefaultRequestHeaders("test-header", "test-value")
                .Use<StubHandler>()
                .Build();

            await httpClient.GetAsync("https://www.test.com");

            var receivedRequest = StubHandler.ReceivedRequest;

            Assert.That(receivedRequest, Is.Not.Null);
            Assert.That(receivedRequest.Headers.Contains("test-header"), Is.True);
            Assert.That(receivedRequest.Headers.GetValues("test-header"), Does.Contain("test-value"));
        }
    }
}