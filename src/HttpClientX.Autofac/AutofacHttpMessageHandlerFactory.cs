using System;
using System.Linq;
using System.Net.Http;
using Autofac;

namespace HttpClientX.Autofac
{
    public class AutofacHttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        private readonly IComponentContext _componentContext; 
        
        public AutofacHttpMessageHandlerFactory(ContainerBuilder containerBuilder)
        {
            _componentContext = containerBuilder.Build();
        }
        
        public HttpMessageHandler Create(Type handlerType, params object[] arguments)
        {
            var parameters = arguments.Select(AsTypedParameter);
            var httpMessageHandler = (HttpMessageHandler) _componentContext.Resolve(handlerType, parameters);
            
            return httpMessageHandler;
        }

        private static TypedParameter AsTypedParameter(object argument)
        {
            var argumentType = argument is HttpMessageHandler
                ? typeof(HttpMessageHandler)
                : argument.GetType();
            
            return new TypedParameter(argumentType, argument);
        }
    }
}