using System;
using System.Net.Http;
using NUnit.Framework;

namespace HttpClientX.Tests
{
    [TestFixture]
    public class HttpMessageHandlerFactoryTests
    {
        [Test]
        public void It_should_be_possible_to_create_an_instance_of_HttpMessageHandler()
        {
            var target = new HttpMessageHandlerFactory();
            var result = target.Create(typeof(HttpClientHandler));

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<HttpClientHandler>());
        }

        [Test]
        public void It_should_be_possible_to_create_an_instance_of_HttpMessageHandler_specifying_its_dependencies()
        {
            var innerHandler = new HttpClientHandler();
            var headerName = "header-name";
            var headerValue = "header-value";

            var target = new HttpMessageHandlerFactory();
            var result = target.Create(typeof(SetResponseHeaderMessageHandler), innerHandler, headerName, headerValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<SetResponseHeaderMessageHandler>());

            var messageHandler = (SetResponseHeaderMessageHandler) result;
            Assert.That(messageHandler.InnerHandler ,Is.EqualTo(innerHandler));
            Assert.That(messageHandler.HeaderName ,Is.EqualTo(headerName));
            Assert.That(messageHandler.HeaderValue ,Is.EqualTo(headerValue));
        }

        [Test]
        public void It_should_not_be_possible_to_create_an_instance_for_a_non_HttpMessageHandler_type()
        {
            var target = new HttpMessageHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => target.Create(typeof(object)));
        }
    }

    
}
