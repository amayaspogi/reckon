namespace reckon.Repository;

public class LookupRepository : ILookupRepository
{
    private readonly int _threshold = 10;
    private readonly IApiService _apiService;

    public LookupRepository(IApiService apiService) => _apiService = apiService;

    public async Task<string[]> GetSubText()
    {
        var tries = 0;
        while (true)
        {
            try
            {
                tries++;
                var result = await _apiService.Get<Keywords>("test2/subTexts");
                return result?.SubTexts ?? Array.Empty<string>();
            }
            catch
            {
                if (tries >= _threshold) throw;
                Thread.Sleep(100 * tries);
            }
        }
    }

    public async Task<string> GetTextToSearch()
    {
        var tries = 0;
        while (true)
        {
            try
            {
                tries++;
                var result = await _apiService.Get<TextToSearch>("test2/textToSearch");
                return result?.Text ?? "";
            }
            catch
            {
                if (tries >= _threshold) throw;
                Thread.Sleep(100 * tries);
            }
        }
    }

    public async Task SubmitLookup(SubmitResult result)
    {
        var tries = 0;
        while (true)
        {
            try
            {
                tries++;
                await _apiService.Post("test2/submitResults", result);
                return;
            }
            catch
            {
                if (tries >= _threshold) throw;
                Thread.Sleep(100 * tries);
            }
        }
    }
}