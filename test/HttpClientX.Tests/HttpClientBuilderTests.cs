using System.Threading.Tasks;
using NUnit.Framework;

namespace HttpClientX.Tests
{
    public class HttpClientBuilderTests
    {
        [Test]
        public async Task It_should_be_possible_to_create_a_HttpClient()
        {
            var httpClient = new HttpClientBuilder()
                .Build();

            var response = await httpClient.GetAsync("http://www.google.com");
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task It_should_be_possible_to_create_a_HttpClient_specifying_multiple_message_handlers()
        {
            var httpClient = new HttpClientBuilder()
                .Use<SetResponseHeaderMessageHandler>("first-header", "first-value")
                .Use<SetResponseHeaderMessageHandler>("second-header", "second-value")
                .Build();

            var response = await httpClient.GetAsync("http://www.google.com");
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.Headers.Contains("first-header"), Is.True);
            Assert.That(response.Headers.Contains("second-header"), Is.True);

        }
    }
}