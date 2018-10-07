using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace HttpClientX
{
    public delegate HttpMessageHandler HttpMessageHandlerFactoryMethod(HttpMessageHandler innerHandler);

    public class HttpClientBuilder
    {
        private readonly IHttpMessageHandlerFactory _httpMessageHandlerFactory;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly Stack<HttpMessageHandlerFactoryMethod> _httpMessageHandlerFactoryMethods;

        public HttpClientBuilder()
        {
            _httpMessageHandlerFactory = new HttpMessageHandlerFactory();
            _httpClientHandler = new HttpClientHandler();
            _httpMessageHandlerFactoryMethods = new Stack<HttpMessageHandlerFactoryMethod>();
        }

        public HttpClientBuilder Use<TMessageHandler>(params object[] arguments)
            where TMessageHandler : HttpMessageHandler
        {
            _httpMessageHandlerFactoryMethods.Push(innerHandler => CreateFactoryMethod<TMessageHandler>(innerHandler, arguments));
            
            return this;
        }

        public IHttpClient Build()
        {
            HttpMessageHandler innerHandler = _httpClientHandler;

            foreach(var factoryMethod in _httpMessageHandlerFactoryMethods)
            {
                innerHandler = factoryMethod(innerHandler);
            }

            return new HttpClientWrapper(innerHandler);
        }
        private HttpMessageHandler CreateFactoryMethod<TMessageHandler>(HttpMessageHandler innerHandler, params object[] arguments)
            where TMessageHandler : HttpMessageHandler
        {
            var constructorArguments = new[] { innerHandler }.Concat(arguments).ToArray();
            var handler = _httpMessageHandlerFactory.Create(typeof(TMessageHandler), constructorArguments);

            return handler;
        }
    }
}