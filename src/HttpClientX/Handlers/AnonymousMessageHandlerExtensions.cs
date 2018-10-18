namespace HttpClientX.Handlers
{
    public static class AnonymousMessageHandlerExtensions
    {
        public static HttpClientBuilder Use(this HttpClientBuilder builder, AnonymousMessageHandlerDelegate messageHandler)
        {
            return builder.Use<AnonymousMessageHandlerInternal>(messageHandler);
        }
    }
}