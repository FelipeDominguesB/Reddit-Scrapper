using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services
{
    public class SubredditPostQueueManagementService : QueueManagementService<SubredditDownloadLink>
    {
        private readonly IRedditScrapperService _redditService;

        public SubredditPostQueueManagementService(IRedditScrapperService redditService)
        {
            _redditService = redditService;
        }
        protected override async Task<bool> HandleValue(SubredditDownloadLink item)
        {
            Console.WriteLine($"Reading post {item.classification}");
            await _redditService.DownloadRedditPost(item);

            Console.WriteLine("Finished handling " + item.classification);
            return true;
        }
    }
}
