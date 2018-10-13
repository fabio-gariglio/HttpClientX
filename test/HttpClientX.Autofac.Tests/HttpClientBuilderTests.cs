using System.Net.Http;
using Autofac;
using HttpClientX.Autofac.Tests.TestHandlers;
using NUnit.Framework;

namespace HttpClientX.Autofac.Tests
{
    public class HttpClientBuilderTests
    {
        [Test]
        public void It_should_be_possible_to_use_Autofac_to_cre()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacTestModule>();

            var container = containerBuilder.Build();

            var httpClient = container.Resolve<HttpClient>();
        }
    }

    public class AutofacTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var httpClient = new HttpClientBuilder()
                .UseAutofac(builder)
                .Build();

            builder.RegisterType<HttpMessageHandlerWithHandlerAndServiceDependency>();
            builder.RegisterType<ServiceImplementation>().As<IService>();
            builder.RegisterInstance(httpClient);
        }
    }
}