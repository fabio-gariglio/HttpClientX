using System.Net.Http;
using Autofac;
using HttpClientX.Autofac.Tests.TestHandlers;
using NUnit.Framework;

namespace HttpClientX.Autofac.Tests
{
    public class AutofacHttpMessageHandlerFactoryTests
    {
        [Test]
        public void It_should_be_possible_to_create_a_message_handler_with_no_dependencies()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpMessageHandlerWithNoDependencies>();

            var factory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            var messageHandler = factory.Create(typeof(HttpMessageHandlerWithNoDependencies));

            Assert.That(messageHandler, Is.Not.Null);
        }

        [Test]
        public void It_should_be_possible_to_ignore_specified_dependencies()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpMessageHandlerWithNoDependencies>();

            var factory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            var messageHandler = factory.Create(typeof(HttpMessageHandlerWithNoDependencies), new HttpClientHandler());

            Assert.That(messageHandler, Is.Not.Null);
        }

        [Test]
        public void It_should_be_possible_to_create_a_message_handler_with_a_value_dependency()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpMessageHandlerWithValueDependency>();

            var factory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            var messageHandler = factory.Create(typeof(HttpMessageHandlerWithValueDependency), "test-value");

            Assert.That(messageHandler, Is.Not.Null);
        }

        [Test]
        public void Specified_message_handler_should_take_priority_over_handlers_registered_in_the_container()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpMessageHandlerWithNoDependencies>();
            containerBuilder.RegisterType<HttpMessageHandlerWithHandlerDependency>();

            var factory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            var messageHandler =
                factory.Create(typeof(HttpMessageHandlerWithHandlerDependency), new HttpClientHandler());

            Assert.That(messageHandler, Is.Not.Null);
        }

        [Test]
        public void It_should_be_possible_to_create_a_message_handler_with_an_handler_and_service_dependency()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<HttpMessageHandlerWithHandlerAndServiceDependency>();
            containerBuilder.RegisterType<ServiceImplementation>().As<IService>();

            var factory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            var messageHandler = factory.Create(typeof(HttpMessageHandlerWithHandlerAndServiceDependency),
                (HttpMessageHandler) new HttpClientHandler());

            Assert.That(messageHandler, Is.Not.Null);
        }
    }
}