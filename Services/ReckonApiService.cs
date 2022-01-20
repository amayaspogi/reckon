namespace reckon.Services;

public class ReckonApiService : IApiService
{
    private readonly HttpClient _httpClient = new();


    public ReckonApiService(IConfiguration configuration)
    {
        _httpClient.BaseAddress = new Uri(
            uriString: configuration.GetValue<string>("ReckonApi"));
    }

    public async Task<T> Get<T>(string route) where T : class, new()
    {
        var response = await _httpClient.SendAsync(new(HttpMethod.Get, route));
        var responseText = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(responseText, options: new(JsonSerializerDefaults.Web)) ?? new();
    }

    public async Task Post<T>(string route, T data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, route)
        {
            Content = JsonContent.Create<T>(data)
        };

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}