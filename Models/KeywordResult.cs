namespace reckon.Models;

public class KeywordResult
{
    public string Text { get; set; }
    public char[] Array { get; set; }
    public int[] Index { get; set; }

    public KeywordResult(string text)
    {
        Text = text;
        Array = text.ToLower().ToArray();
        Index = System.Array.Empty<int>();
    }
}