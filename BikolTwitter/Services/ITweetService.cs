using BikolTwitter.Dtos;

namespace BikolTwitter.Services;

public interface ITweetService
{
    Task<IEnumerable<TweetDto>> GetAllTweetsAsync();
}
