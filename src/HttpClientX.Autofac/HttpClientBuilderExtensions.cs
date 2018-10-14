using Autofac;

namespace HttpClientX.Autofac
{
    public static class HttpClientBuilderExtensions
    {
        public static HttpClientBuilder UseAutofac(this HttpClientBuilder builder, IComponentContext context)
        {
            var httpMessageHandlerFactory = new AutofacHttpMessageHandlerFactory(context);
            
            return builder.UseHandlerFactory(httpMessageHandlerFactory);
        }
    }
}