using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Workers
{
    public class ReadSubredditWorkerService : IWorkerService
    {

        private readonly IRedditScrapperService _redditService;
        private readonly IQueueService<SubredditDownloadLink> _queueService;

        public ReadSubredditWorkerService(IRedditScrapperService redditService, IQueueService<SubredditDownloadLink> queueService)
        {
            _redditService = redditService;
            _queueService = queueService;
        }

        public async Task<bool> Start()
        {
            string subredditName = "booty";
            ICollection<SubredditDownloadLink> subredditLinks = await _redditService.ReadSubredditData(subredditName);

            foreach (SubredditDownloadLink subredditDownloadLink in subredditLinks)
            {
                try
                {
                    _queueService.Publish(subredditDownloadLink);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading queue. Message: " + ex.Message);
                    throw;
                }
            }

            return true;
        }

        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}
