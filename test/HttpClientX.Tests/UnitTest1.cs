using System.Threading.Tasks;
using NUnit.Framework;

namespace HttpClientX.Tests
{
    public class HttpClientBuilder
    {
        public IHttpClient Build()
        {
            return new HttpClientWrapper();
        }
    }

    public class Tests
    {
        [Test]
        public async Task It_should_be_possible_to_create_a_HttpClient()
        {
            var httpClient = new HttpClientBuilder().Build();

            var response = await httpClient.GetAsync("http://www.google.com");
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }
    }
}