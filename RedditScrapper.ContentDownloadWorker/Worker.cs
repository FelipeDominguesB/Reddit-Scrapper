using RedditScrapper.Model.Message;
using RedditScrapper.Services.Queue;
using RedditScrapper.Services.Routines;

namespace RedditScrapper.ContentDownloadWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueService<RedditPostMessage> _queueService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IQueueService<RedditPostMessage> queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _queueService.Read();

            await Task.Delay(-1, stoppingToken);
            
        }
    }
}