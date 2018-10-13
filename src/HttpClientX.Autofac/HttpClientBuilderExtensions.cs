using Autofac;

namespace HttpClientX.Autofac
{
    public static class HttpClientBuilderExtensions
    {
        public static HttpClientBuilder UseAutofac(this HttpClientBuilder builder, ContainerBuilder containerBuilder)
        {
            var httpMessageHandlerFactory = new AutofacHttpMessageHandlerFactory(containerBuilder);
            
            return builder.UseHandlerFactory(httpMessageHandlerFactory);
        }
    }
}