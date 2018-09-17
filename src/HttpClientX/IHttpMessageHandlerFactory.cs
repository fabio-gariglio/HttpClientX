using System;
using System.Net.Http;

namespace HttpClientX
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler Create(Type handlerType, params object[] arguments);
    }
}
