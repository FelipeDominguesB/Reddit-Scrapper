using RedditScrapper.Model;
using RedditScrapper.Services;
using RedditScrapper.Services.Worker;

namespace RedditScrapper.ContentFetchWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerService _workerService;

        public Worker(ILogger<Worker> logger, IWorkerService workerService)
        {
            _logger = logger;
            _workerService = workerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime whenToRun = DateTime.UtcNow;

            while (true)
            {
                if(DateTime.UtcNow > whenToRun)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    Console.WriteLine("Worker starting");
                    await _workerService.Start();
                    whenToRun = whenToRun.AddMinutes(3);
                }
            }
        }
    }
}