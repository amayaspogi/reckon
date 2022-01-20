namespace reckon.Models;

public class LookUpResult
{
    public string Subtext { get; }
    public string Result { get; }

    public LookUpResult(string subtext, int[] index)
    {
        Subtext = subtext;
        Result = index.Any() ? string.Join(", ", index) : "<No Output>";
    }
}