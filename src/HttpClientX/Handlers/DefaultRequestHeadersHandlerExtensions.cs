namespace HttpClientX.Handlers
{
    public static class DefaultRequestHeadersHandlerExtensions
    {
        public static HttpClientBuilder UseDefaultRequestHeaders(this HttpClientBuilder builder, string headerName, string headerValue)
        {
            return builder.Use<DefaultRequestHeadersHandler>(headerName, headerValue);
        }
    }
}