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
        private bool _currentlyGetting = false;

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
            if (CheckCurrentlyGetting())
            {
                return;
            }

            try
            {
                var bikolSubs = await _dbContext.BikolSubs.ToListAsync();
                var currentTweets = await _dbContext.BikolSubTweets.ToListAsync();
                var latestTweet = currentTweets.Any() ? currentTweets.Max(t => t.CreatedAt) : default;
                if (latestTweet < DateTimeOffset.UtcNow.AddHours(-24))
                {
                    latestTweet = DateTimeOffset.UtcNow.AddHours(-24);
                }
                var newTweets = new List<BikolSubTweet>();
                foreach (var bikolSub in bikolSubs)
                {
                    var tweets = await GetBikolSubTweetsAsync(bikolSub.Username, latestTweet);
                    newTweets.AddRange(tweets);
                }

                if (!newTweets.Any())
                {
                    return;
                }

                await _dbContext.BikolSubTweets.AddRangeAsync(newTweets);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Getting new tweets from Twitter API falied, exception caught.", e);
            }
            finally
            {
                _currentlyGetting = false;
            }
        }

        private readonly object _currentlyGettingLockObj = new object();
        private bool CheckCurrentlyGetting()
        {
            lock (_currentlyGettingLockObj)
            {
                if (_currentlyGetting)
                {
                    return true;
                }

                _currentlyGetting = true;
                return false;
            }
        }

        private async Task<IEnumerable<BikolSubTweet>> GetBikolSubTweetsAsync(string username, DateTimeOffset lastTweetDate)
        {
            try
            {
                var tweets = (await _timelinesClient.GetUserTimelineAsync(username))
                             .Where(t => t.CreatedAt > lastTweetDate).ToList();
                return tweets.Select(t => new BikolSubTweet
                {
                    CreatedAt = t.CreatedAt,
                    Text = t.Text,
                    FullText = t.FullText,
                    Prefix = t.Prefix,
                    Suffix = t.Suffix,
                    FavoriteCount = t.FavoriteCount,
                    CreatedBy = t.CreatedBy.Name
                });
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
            _currentlyGetting = false;
        }
    }
}
