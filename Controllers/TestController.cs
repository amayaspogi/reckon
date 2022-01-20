namespace reckon.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ISearchService _searchService;

    public TestController(ISearchService searchService) => _searchService = searchService;

    [HttpGet(Name = nameof(Get))]
    public async Task<SubmitResult> Get()
    {
        return await _searchService.SubmitLookup();
    }
}