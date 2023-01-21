using Tweetinvi.Client;
using Tweetinvi.Core.Models;
using Tweetinvi;
using Tweetinvi.Models;
using BikolTwitter.Dtos;
using BikolTwitter.Entities;
using BikolTwitter.Database;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BikolTwitter.Services;

public class TweetService : ITweetService
{
    private readonly BikolTwitterDbContext _dbContext;
    private readonly IMapper _mapper;

    public TweetService(BikolTwitterDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TweetDto>> GetAllTweetsAsync(DateTimeOffset from)
    {
        var tweets = (await _dbContext
                           .BikolSubTweets
                           .ToListAsync())
                           .Where(t => t.CreatedAt > from)
                           .Take(100);
        return _mapper.Map<IEnumerable<TweetDto>>(tweets);
    }
}
