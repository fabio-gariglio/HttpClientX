using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Handlers
{
    public delegate Task<HttpResponseMessage> AnonymousMessageHandler(Func<Task<HttpResponseMessage>> nextHandler, HttpRequestMessage request, CancellationToken cancellationToken);
}