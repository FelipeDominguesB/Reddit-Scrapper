using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Workers
{
    public class ContentDownloadWorkerService : IWorkerService
    {
        private readonly IQueueService<SubredditDownloadLink> _queueService;
        
        public ContentDownloadWorkerService(IQueueService<SubredditDownloadLink> queueService)
        {
            _queueService = queueService;
        }

        public async Task<bool> Start()
        {
            try
            {
                _queueService.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading queue. Message: " + ex.Message);
            }
            return true;
        }

        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}
