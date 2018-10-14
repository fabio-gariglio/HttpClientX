using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using HttpClientX.Autofac.Tests.TestHandlers;
using NUnit.Framework;

namespace HttpClientX.Autofac.Tests
{
    public class HttpClientBuilderTests
    {
        [Test]
        public async Task It_should_be_possible_to_use_Autofac_to_cre()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacTestModule>();

            var container = containerBuilder.Build();

            var httpClient = container.Resolve<HttpClient>();
            await httpClient.GetAsync("http://test.com");

            Assert.That(HttpMessageHandlerWithHandlerAndServiceDependency.ReceivedRequest, Is.Not.Null);
            Assert.That(HttpMessageHandlerWithHandlerAndServiceDependency.Handler, Is.Not.Null);
            Assert.That(HttpMessageHandlerWithHandlerAndServiceDependency.Service, Is.Not.Null);
        }
    }

    public class AutofacTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpMessageHandlerWithHandlerAndServiceDependency>();
            builder.RegisterType<ServiceImplementation>().As<IService>();
            builder.Register(BuildHttpClient).SingleInstance();
        }

        private static HttpClient BuildHttpClient(IComponentContext context)
        {
            var httpClient = new HttpClientBuilder()
                .UseAutofac(context)
                .Use<HttpMessageHandlerWithHandlerAndServiceDependency>()
                .Build();

            return httpClient;
        }

    }
}
