using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientX.Handlers
{
    public delegate Task<HttpResponseMessage> MessageHandlerDelegate(HttpRequestMessage request, CancellationToken cancellationToken);
}