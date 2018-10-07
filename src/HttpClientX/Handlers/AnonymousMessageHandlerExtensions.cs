namespace HttpClientX.Handlers
{
    public static class AnonymousMessageHandlerExtensions
    {
        public static HttpClientBuilder Use(this HttpClientBuilder builder, AnonymousMessageHandler messageHandler)
        {
            return builder.Use<AnonymousMessageHandlerInternal>(messageHandler);
        }
    }
}