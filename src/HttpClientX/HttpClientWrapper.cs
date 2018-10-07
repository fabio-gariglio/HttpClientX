using System.Net.Http;

namespace HttpClientX
{
    internal class HttpClientWrapper : HttpClient, IHttpClient
    {
        /// <summary>Initializes a new instance of the <see cref="T:HttpClientX.HttpClientWrapper"></see> class.</summary>
        public HttpClientWrapper() : base()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:HttpClientX.HttpClientWrapper"></see> class with a specific handler.</summary>
        /// <param name="handler">The HTTP handler stack to use for sending requests.</param>
        public HttpClientWrapper(HttpMessageHandler handler) : base(handler)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:HttpClientX.HttpClientWrapper"></see> class with a specific handler.</summary>
        /// <param name="handler">The <see cref="T:System.Net.Http.HttpMessageHandler"></see> responsible for processing the HTTP response messages.</param>
        /// <param name="disposeHandler">true if the inner handler should be disposed of by Dispose(), false if you intend to reuse the inner handler.</param>
        public HttpClientWrapper(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        {
        }
    }
}
