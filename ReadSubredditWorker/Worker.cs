using RedditScrapper.Model;
using RedditScrapper.Services;

namespace ReadSubredditWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RedditService _redditService;
        public Worker(ILogger<Worker> logger, RedditService redditService)
        {
            _logger = logger;
            _redditService = redditService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                List<SubredditDownloadLink> links = await _redditService.ReadSubredditData("pics");
                await _redditService.DownloadSubredditData(links);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}