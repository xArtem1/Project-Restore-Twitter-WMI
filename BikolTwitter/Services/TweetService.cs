using Tweetinvi.Client;
using Tweetinvi.Core.Models;
using Tweetinvi;
using Tweetinvi.Models;
using BikolTwitter.Dtos;

namespace BikolTwitter.Services;

public class TweetService : ITweetService
{
    private readonly ITimelinesClient _timelinesClient;

    public TweetService(ITimelinesClient timelinesClient)
    {
        _timelinesClient = timelinesClient;
    }

    //TODO: This method so far will return only fake tweets
    public async Task<IEnumerable<TweetDto>> GetAllTweetsAsync()
    {
        try
        {
            var tweets = await _timelinesClient.GetUserTimelineAsync("@elonmusk");
        }
        catch (Exception e)
        {

            throw;
        }
        return null;
    }
}
