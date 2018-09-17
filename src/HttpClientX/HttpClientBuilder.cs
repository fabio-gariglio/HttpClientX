namespace HttpClientX
{
    public class HttpClientBuilder
    {
        public IHttpClient Build()
        {
            return new HttpClientWrapper();
        }
    }
}