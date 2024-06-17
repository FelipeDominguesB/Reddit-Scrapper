using Microsoft.Extensions.Logging;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.Services.Queue;
using RedditScrapper.Services.Scrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Routines
{
    public class RoutineExecutionService : IRoutineExecutionService
    {
        private readonly ILogger<RoutineExecutionService> _logger;
        private readonly IQueueService<RedditPostMessage> _queueService;
        private readonly IRedditScrapperService _redditScrapperService;
        private readonly IRoutineManagementService _routineManagementService;
        public RoutineExecutionService(ILogger<RoutineExecutionService> logger, 
            IRedditScrapperService redditScrapperService, 
            IQueueService<RedditPostMessage> queueService,
            IRoutineManagementService routineManagementService) 
        {
            _logger = logger;
            _queueService = queueService;
            _redditScrapperService = redditScrapperService;
            _routineManagementService = routineManagementService;
        }

        public async Task RunPendingRoutines()
        {
            ICollection<Routine> pendingRoutines = await _routineManagementService.GetPendingRoutines();

            foreach (Routine routine in pendingRoutines)
                await RunRoutine(routine);
        }

        public async Task RunRoutine(Routine routine)
        {

            int totalLinksFound = 0;
            try
            {
                RoutineExecutionDTO routineExecutionDTO = new RoutineExecutionDTO()
                {
                    RoutineId = routine.Id,
                    MaxPostsPerSync = routine.MaxPostsPerSync,
                    PostSorting = routine.PostSorting,
                    SyncRate = routine.SyncRate,
                };

                routineExecutionDTO = await _routineManagementService.AddRoutineExecution(routineExecutionDTO);

                ICollection<RedditPostMessage> subredditLinks = await _redditScrapperService.ReadSubredditPosts(routine.SubredditName, routine.MaxPostsPerSync, (SortingEnum)routine.PostSorting);
                totalLinksFound = subredditLinks.Count;

                foreach (RedditPostMessage subredditDownloadLink in subredditLinks)
                {
                    subredditDownloadLink.ExecutionId = routineExecutionDTO.Id;
                    _queueService.Publish(subredditDownloadLink);
                }


                routineExecutionDTO.TotalLinksFound = totalLinksFound;
                routineExecutionDTO.Succeded = true;

                routineExecutionDTO = await _routineManagementService.UpdateRoutineExecution(routineExecutionDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception reading queue. Message: " + ex.Message);
            }

        }
    }
}
