namespace reckon.Models;

public class SubmitResult
{
    public string Candidate { get; set; } = "";
    public string Text { get; set; } = "";
    public LookUpResult[] Results { get; set; } = Array.Empty<LookUpResult>();
}
