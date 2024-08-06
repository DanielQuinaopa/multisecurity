namespace multitrabajo_deposito.Services
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);
    }
}
