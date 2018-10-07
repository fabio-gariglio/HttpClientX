using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HttpClientX.Tests
{
    public class HttpClientBuilderTests
    {
        [Test]
        public void It_should_be_possible_to_create_an_IHttpClient_interface_to_simplify_mocking()
        {
            var httpClient = new HttpClientBuilder()
                .Build();
            
            Assert.That(httpClient, Is.AssignableTo<IHttpClient>());
        }
        
        [Test]
        public void It_should_be_possible_to_create_an_HttpClient_instance_so_existing_code_does_not_need_to_change()
        {
            var httpClient = new HttpClientBuilder()
                .Build();
            
            Assert.That(httpClient, Is.AssignableTo<HttpClient>());
        }
        
        [Test]
        public async Task It_should_be_possible_to_use_the_HttpClient_built()
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