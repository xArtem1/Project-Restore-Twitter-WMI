using AutoMapper;
using BikolTwitter.Database;
using BikolTwitter.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tweetinvi.Client;
using Tweetinvi.Models;

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
                var lockObj = new object();
                var newTweets = new List<ITweet>();
                void AddTweets(IEnumerable<ITweet> tweets)
                {
                    lock (lockObj)
                    {
                        newTweets.AddRange(tweets);
                    }
                }

                await Task.WhenAll(bikolSubs.Select(async s =>
                {
                    try
                    {
                        var allTweets = await _timelinesClient.GetUserTimelineAsync(s.Username);
                        AddTweets(allTweets.Where(t => t.CreatedAt >= DateTimeOffset.UtcNow.AddHours(-24)));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Couldn't load tweets of bikolsub {s.Username}.", e);
                    }
                }));

                _dbContext.BikolSubTweets.RemoveRange(await _dbContext.BikolSubTweets.ToListAsync());
                await _dbContext.BikolSubTweets.AddRangeAsync(newTweets.Where(t => t.FullText.Length > 10).Select(t => new BikolSubTweet
                {
                    CreatedAt = t.CreatedAt,
                    Text = t.Text,
                    FullText = t.FullText,
                    Prefix = t.Prefix,
                    Suffix = t.Suffix,
                    FavoriteCount = t.FavoriteCount,
                    CreatedBy = t.CreatedBy.Name,
                    CreatedByScreenName = t.CreatedBy.ScreenName
                }));
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
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _currentlyGetting = false;
        }
    }
}
