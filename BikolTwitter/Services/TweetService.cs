using Tweetinvi.Client;
using Tweetinvi.Core.Models;
using Tweetinvi;
using Tweetinvi.Models;
using BikolTwitter.Dtos;

namespace BikolTwitter.Services;

public class TweetService : ITweetService
{
    private readonly ITimelinesClient _timelinesClient;

    public TweetService(/*ITimelinesClient timelinesClient*/)
    {
        //_timelinesClient = timelinesClient;
    }

    //TODO: This method so far will return only fake tweets
    public async Task<IEnumerable<TweetDto>> GetAllTweetsAsync()
    {
        return new TweetDto[]
        {
            new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user1",
                CreatedAt = DateTimeOffset.Now,
                FavoriteCount = 10
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user2",
                CreatedAt = DateTimeOffset.Now.AddMinutes(1),
                FavoriteCount = 30
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user3",
                CreatedAt = DateTimeOffset.Now.AddMinutes(2),
                FavoriteCount = 15
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user4",
                CreatedAt = DateTimeOffset.Now.AddMinutes(3),
                FavoriteCount = 60
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user5",
                CreatedAt = DateTimeOffset.Now.AddMinutes(4),
                FavoriteCount = 90
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user6",
                CreatedAt = DateTimeOffset.Now.AddMinutes(5),
                FavoriteCount = 10
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user7",
                CreatedAt = DateTimeOffset.Now.AddMinutes(6),
                FavoriteCount = 12
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user8",
                CreatedAt = DateTimeOffset.Now.AddMinutes(7),
                FavoriteCount = 50
            }, new TweetDto
            {
                Text = "This is some content of the tweet",
                FullText = "This is some extended content of the tweet",
                Prefix = "prefix",
                Suffix = "suffix",
                CreatedBy = "@user9",
                CreatedAt = DateTimeOffset.Now.AddMinutes(8),
                FavoriteCount = 27
            },
        };
    }
}
