namespace BikolTwitter.Dtos;

/// <summary>
/// Class reprezenting data about tweet, which are returned to clinent.
/// </summary>
public record TweetDto
{
    public DateTimeOffset CreatedAt { get; init; }
    public string Text { get; set; }
    public string FullText { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public int FavoriteCount { get; set; }
    public string CreatedBy { get; set; }   
}
