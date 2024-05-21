using RedditScrapper.Model.Message;
using RedditScrapper.Services.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Worker
{
    public class ContentDownloadWorkerService : IWorkerService
    {
        private readonly IQueueService<RedditPostMessage> _queueService;

        public ContentDownloadWorkerService(IQueueService<RedditPostMessage> queueService)
        {
            _queueService = queueService;
        }

        public async Task Run()
        {
            try
            {
                _queueService.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading queue. Message: " + ex.Message);
                throw;
            }
        }


    }
}
