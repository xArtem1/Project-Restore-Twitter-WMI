namespace BikolTwitter.Entities;

public class BikolSubTweet
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public string Text { get; set; }
    public string FullText { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    public int FavoriteCount { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedByScreenName { get; set; }
}
