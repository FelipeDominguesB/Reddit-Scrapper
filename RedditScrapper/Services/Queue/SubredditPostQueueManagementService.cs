using RedditScrapper.Model;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy.Model;
using RedditScrapper.Services.Scrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Queue
{
    public class SubredditPostQueueManagementService : QueueManagementService<RedditPostMessage>
    {
        private readonly IRedditScrapperService _redditService;

        public SubredditPostQueueManagementService(IRedditScrapperService redditService)
        {
            _redditService = redditService;
        }
        protected override async Task<bool> HandleValue(RedditPostMessage item)
        {
            Console.WriteLine($"Reading post {item.Classification}");
            await _redditService.DownloadRedditPost(item);
            Console.WriteLine("Finished handling " + item.Classification);
            return true;
        }
    }
}
