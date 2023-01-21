using AutoMapper;
using BikolTwitter.Database;
using BikolTwitter.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tweetinvi.Client;

namespace BikolTwitter.Background
{
    public class TwitterAPIBackgroundReader
    {
        private bool _isRunning;

        private readonly ITimelinesClient _timelinesClient;
        private readonly BikolTwitterDbContext _dbContext;
        private readonly ILogger<TwitterAPIBackgroundReader> _logger;
        private readonly IMapper _mapper;

        public TwitterAPIBackgroundReader(ITimelinesClient timelinesClient, BikolTwitterDbContext dbContext,
            ILogger<TwitterAPIBackgroundReader> logger, IMapper mapper)
        {
            _timelinesClient = timelinesClient;
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }


        private async Task GetNewTweetsAsync()
        {
            try
            {
                var bikolSubs = await _dbContext.BikolSubs.ToListAsync();
                var currentTweets = await _dbContext.BikolSubTweets.ToListAsync();
                var latestTweet = currentTweets.Any() ? currentTweets.Max(t => t.CreatedAt) : default;
                if (latestTweet < DateTimeOffset.UtcNow.AddHours(-24))
                {
                    latestTweet = DateTimeOffset.UtcNow.AddHours(-24);
                }
                var newTweets = new List<object>();
                foreach (var bikolSub in bikolSubs)
                {
                    var tweets = await GetBikolSubTweetsAsync(bikolSub.Username, latestTweet);
                    newTweets.AddRange(tweets);
                }

                if (!newTweets.Any())
                {
                    return;
                }

                var tweetsEntities = _mapper.Map<IEnumerable<BikolSubTweet>>(newTweets).ToList();
                await _dbContext.BikolSubTweets.AddRangeAsync(tweetsEntities);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Getting new tweets from Twitter API falied, exception caught.", e);
            }
        }

        private async Task<IEnumerable<BikolSubTweet>> GetBikolSubTweetsAsync(string username, DateTimeOffset lastTweetDate)
        {
            try
            {
                var tweets = (await _timelinesClient.GetUserTimelineAsync(username))
                             .Where(t => t.CreatedAt > lastTweetDate);
                return _mapper.Map<IEnumerable<BikolSubTweet>>(tweets);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception caught when trying to get {username} tweets.", e);
                return Enumerable.Empty<BikolSubTweet>();
            }
        }

        public async void Start()
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;
            while (_isRunning)
            {
                await GetNewTweetsAsync();
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
