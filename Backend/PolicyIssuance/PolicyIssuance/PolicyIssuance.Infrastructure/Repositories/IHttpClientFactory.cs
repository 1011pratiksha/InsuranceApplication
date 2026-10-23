namespace PolicyIssuance.Infrastructure.Services
{
    public interface IHttpClientFactory
    {
        object CreateClient(string v);
    }
}