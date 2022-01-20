namespace reckon.Services;

public interface ISearchService
{
    Task<SubmitResult> SubmitLookup();
}