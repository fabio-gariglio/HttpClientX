using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
            var stringProperty = "test-value";
            var timeSpanProperty = TimeSpan.FromDays(1);

            var target = new HttpMessageHandlerFactory();
            var result = target.Create(typeof(HavePropertiesMessageHandler), innerHandler, stringProperty, timeSpanProperty);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<HavePropertiesMessageHandler>());

            var messageHandler = (HavePropertiesMessageHandler) result;
            Assert.That(messageHandler.InnerHandler ,Is.EqualTo(innerHandler));
            Assert.That(messageHandler.StringProperty ,Is.EqualTo(stringProperty));
            Assert.That(messageHandler.TimeSpanProperty ,Is.EqualTo(timeSpanProperty));
        }

        [Test]
        public void It_should_not_be_possible_to_create_an_instance_for_a_non_HttpMessageHandler_type()
        {
            var target = new HttpMessageHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => target.Create(typeof(object)));
        }
        
        public class HavePropertiesMessageHandler : DelegatingHandler
        {
            public string StringProperty { get; }
            public TimeSpan TimeSpanProperty { get; }

            public HavePropertiesMessageHandler(HttpMessageHandler innerHandler, string stringProperty, TimeSpan timeSpanProperty) : base(innerHandler)
            {
                StringProperty = stringProperty;
                TimeSpanProperty = timeSpanProperty;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    
}
