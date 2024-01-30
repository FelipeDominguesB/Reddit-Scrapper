using RedditScrapper.Context;
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
        private readonly RedditScrapperContext _dbContext;

        public SubredditPostQueueManagementService(IRedditScrapperService redditService, RedditScrapperContext dbContext)
        {
            _redditService = redditService;
            _dbContext = dbContext;
        }
        protected override async Task<bool> HandleValue(RedditPostMessage item)
        {
            Console.WriteLine($"Reading post {item.Classification}");
            bool downloadSucceded =  await _redditService.DownloadRedditPost(item);

            if(downloadSucceded)
            {
                
            }
            Console.WriteLine("Finished handling " + item.Classification);
            return true;
        }
    }
}
