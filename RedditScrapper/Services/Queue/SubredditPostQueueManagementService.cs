using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RedditScrapper.Configuration;
using RedditScrapper.Context;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient.Model;
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
        private readonly IRoutineManagementService _routineService;

        public SubredditPostQueueManagementService(
            IRedditScrapperService redditService, 
            IRoutineManagementService routineService, 
            IConfiguration configuration, 
            ILogger<SubredditPostQueueManagementService> logger) : base(configuration, logger)
        {
            _redditService = redditService;
            _routineService = routineService;
        }

        protected override async Task<bool> HandleValue(RedditPostMessage item)
        {
            RoutineExecutionFileDTO routineExecutionFileDTO;

            try
            {
                routineExecutionFileDTO = await _redditService.DownloadRedditPost(item);
            }
            catch (Exception ex) {

                routineExecutionFileDTO = new RoutineExecutionFileDTO()
                {
                    Classification = item.Classification,
                    SourceUrl = item.Url ?? "NO URL",
                    RoutineExecutionId = item.ExecutionId,
                    Succeded = false
                };
            }

            await _routineService.AddRoutineExecutionFile(routineExecutionFileDTO);

            return routineExecutionFileDTO.Succeded;
        }
    }
}
