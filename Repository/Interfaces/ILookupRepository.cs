namespace reckon.Repository;

public interface ILookupRepository
{
    Task<string> GetTextToSearch();
    Task<string[]> GetSubText();
    Task SubmitLookup(SubmitResult result);
}