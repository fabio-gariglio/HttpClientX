using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Handlers
{
    internal class AnonymousMessageHandlerInternal : DelegatingHandler
    {
        private readonly AnonymousMessageHandler _anonymousMessageHandler;

        public AnonymousMessageHandlerInternal(HttpMessageHandler nextHandler, AnonymousMessageHandler anonymousMessageHandler) : base(nextHandler)
        {
            _anonymousMessageHandler = anonymousMessageHandler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _anonymousMessageHandler(() => base.SendAsync(request, cancellationToken), request, cancellationToken);
        }
    }
}