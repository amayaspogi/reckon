namespace reckon.Services;

public class SearchService : ISearchService
{
    private readonly IConfiguration _configuration;
    private readonly ILookupRepository _lookupRepository;

    public SearchService(IConfiguration configuration, ILookupRepository lookupRepository)
        => (_configuration, _lookupRepository) = (configuration, lookupRepository);

    public async Task<SubmitResult> SubmitLookup()
    {
        var search = await _lookupRepository.GetTextToSearch();
        var subtexts = await _lookupRepository.GetSubText();

        var result = new SubmitResult()
        {
            Candidate = _configuration.GetValue<string>("Candidate"),
            Text = search,
            Results = LookUp(search, subtexts)
                .Select(x => new LookUpResult(x.Text, x.Index))
                .ToArray()
        };

        await _lookupRepository.SubmitLookup(result);
        return result;
    }

    private static KeywordResult[] LookUp(string search, string[] subtexts)
    {
        // start code
        var arrSearch = search.ToLower().ToArray();
        var arrSubtext = subtexts.Select(x => new KeywordResult(x)).ToList();

        // populate start index
        for (var x = 0; x < arrSearch.Length; x++)
        {
            foreach (var y in arrSubtext)
            {
                if (arrSearch[x] == y.Array[0])
                {
                    y.Index = new List<int>(y.Index) { x }.ToArray();
                }
            }
        }

        // check the rest of the text
        foreach (var x in arrSubtext)
        {
            for (var y = 0; y < x.Index.Length; y++)
            {
                // remove index when out of bound
                if (x.Index[y] + x.Array.Length > arrSearch.Length)
                {
                    x.Index[y] = -1;
                    continue;
                }

                // remove index when text not match
                for (var z = 0; z < x.Array.Length; z++)
                {
                    if (x.Array[z] != arrSearch[x.Index[y] + z])
                    {
                        x.Index[y] = -1;
                        break;
                    }
                }
            }
        }

        // clean up
        return arrSubtext
            .Select(x => new KeywordResult(x.Text)
            {
                Index = x.Index
                    .Where(y => y >= 0)
                    .Select(y => y + 1)
                    .ToArray()
            })
            .ToArray();
    }
}