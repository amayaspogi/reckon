namespace reckon.Services;

public interface IApiService
{
    Task<T> Get<T>(string url) where T : class, new();
    Task Post<T>(string route, T data);
}