using RedditScrapper.Context;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy.Model;
using RedditScrapper.Services.Routines;
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
        private readonly IRoutineService _routineService;

        public SubredditPostQueueManagementService(IRedditScrapperService redditService, IRoutineService routineService)
        {
            _redditService = redditService;
            _routineService = routineService;
        }

        protected override async Task<bool> HandleValue(RedditPostMessage item)
        {
            Console.WriteLine($"Reading post {item.Classification}");
            RoutineExecutionFileDTO routineExecutionFileDTO =  await _redditService.DownloadRedditPost(item);

            if(routineExecutionFileDTO.Succeded)
                await _routineService.AddRoutineExecutionFile(routineExecutionFileDTO);

            Console.WriteLine("Finished handling " + item.Classification);
            return true;
        }
    }
}
