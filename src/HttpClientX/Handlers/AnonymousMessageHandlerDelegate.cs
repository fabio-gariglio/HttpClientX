using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Handlers
{
    public delegate Task<HttpResponseMessage> AnonymousMessageHandlerDelegate(MessageHandlerDelegate nextHandler, HttpRequestMessage request, CancellationToken cancellationToken);
}