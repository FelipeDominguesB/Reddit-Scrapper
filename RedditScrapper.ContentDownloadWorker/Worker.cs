using RedditScrapper.Services.Worker;

namespace RedditScrapper.ContentDownloadWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerService _workerService;

        public Worker(ILogger<Worker> logger, IWorkerService workerService, IConfiguration configuration)
        {
            _logger = logger;
            _workerService = workerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _workerService.Start();
            }
            catch(OperationCanceledException ex)
            {
                _logger.LogError("Operation canceled exception. Message: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected exception. Message: " + ex.Message);
            }   
        }
    }
}