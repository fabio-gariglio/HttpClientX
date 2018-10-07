using System.Net.Http;
using System.Threading.Tasks;
using HttpClientX.Tests.Handlers;
using HttpClientX.Tests.Handlers.Tests;
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
        public async Task It_should_be_possible_to_create_a_HttpClient_specifying_a_message_handler()
        {
            var httpClient = new HttpClientBuilder()
                .Use<StubHandler>()
                .Build();

            var response = await httpClient.GetAsync("https://www.test.com");
            
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(StubHandler.ReceivedRequest, Is.Not.Null);
        }
        
        [Test]
        public async Task It_should_be_possible_to_invoke_message_handlers_based_on_the_order_of_definition()
        {
            var httpClient = new HttpClientBuilder()
                .Use<FirstPassThroughHandler>()
                .Use<SecondPassThroughHandler>()
                .Use<StubHandler>()
                .Build();

            await httpClient.GetAsync("https://www.test.com");
            
            Assert.That(FirstPassThroughHandler.ExecutedDateTime, Is.LessThan(SecondPassThroughHandler.ExecutedDateTime));
        }
    }
}