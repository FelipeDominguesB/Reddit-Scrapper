using RedditScrapper.Interface;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Workers
{
    public class ContentDownloadWorkerService : IWorkerService
    {
        private readonly IQueueService<RedditPostMessage> _queueService;
        
        public ContentDownloadWorkerService(IQueueService<RedditPostMessage> queueService)
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
