using System;
using System.Net.Http;

namespace HttpClientX
{
    internal class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        private static readonly Type HttpMessageHandlerType = typeof(HttpMessageHandler);

        public HttpMessageHandler Create(Type handlerType, params object[] arguments)
        {
            EnsureIsHttpMessageHandlerType(handlerType);

            var handler = (HttpMessageHandler)Activator.CreateInstance(handlerType, arguments);

            return handler;
        }

        private static void EnsureIsHttpMessageHandlerType(Type handlerType)
        {
            if (handlerType.IsSubclassOf(HttpMessageHandlerType)) return;

            throw new InvalidOperationException($"{handlerType.Name} must inherit from {HttpMessageHandlerType.Name}");
        }
    }
}
