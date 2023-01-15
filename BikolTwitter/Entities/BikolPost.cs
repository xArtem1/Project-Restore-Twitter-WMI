namespace BikolTwitter.Entities;

/// <summary>
/// Class representing twitter post, wchich was sold by a customer.
/// This is an database entity.
/// </summary>
public class BikolPost
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Identifier of twitter post.
    /// </summary>
    public string TwitterId { get; set; }
    /// <summary>
    /// Date of post publication.
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Amount the post was sold for.
    /// </summary>
    public decimal Profit { get; set; }
    /// <summary>
    /// Foreign key to bikolsub.
    /// </summary>
    public int BikolSubId { get; set; }
    /// <summary>
    /// Bikolsub included by foreign key.
    /// </summary>
    public virtual BikolSub BikolSub { get; set; }
}
