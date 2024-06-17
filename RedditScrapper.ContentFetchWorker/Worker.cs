using RedditScrapper.Services.Routines;

namespace RedditScrapper.ContentFetchWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRoutineExecutionService _routineExecutionService;
        public Worker(ILogger<Worker> logger, IRoutineExecutionService routineExecutionService)
        {
            _logger = logger;
            _routineExecutionService = routineExecutionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime whenToRun = DateTime.UtcNow;
            
            while (!stoppingToken.IsCancellationRequested)
            {

                await _routineExecutionService.RunPendingRoutines();
                await Task.Delay(1000 * 60 * 3, stoppingToken);
            }
        }
    }
}