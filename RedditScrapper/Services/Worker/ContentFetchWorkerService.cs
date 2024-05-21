using Microsoft.Extensions.Logging;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient.Model;
using RedditScrapper.Services.Queue;
using RedditScrapper.Services.Routines;
using RedditScrapper.Services.Scrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Worker
{
    public class ContentFetchWorkerService : IWorkerService
    {

        private readonly IRedditScrapperService _redditService;
        private readonly IRoutineService _routineService;
        private readonly ILogger<ContentFetchWorkerService> _logger;
        private readonly IQueueService<RedditPostMessage> _queueService;

        public ContentFetchWorkerService(ILogger<ContentFetchWorkerService> logger, IRedditScrapperService redditService, IQueueService<RedditPostMessage> queueService, IRoutineService routineService)
        {
            _routineService = routineService;
            _redditService = redditService;
            _queueService = queueService;
            _logger = logger;
        }

        public async Task Run()
        {

            ICollection<Routine> pendingRoutines = await _routineService.GetPendingRoutines();

            foreach (Routine routine in pendingRoutines)
                await RunRoutine(routine);

        }

        private async Task RunRoutine(Routine routine)
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

                routineExecutionDTO = await _routineService.AddRoutineExecution(routineExecutionDTO);

                ICollection<RedditPostMessage> subredditLinks = await _redditService.ReadSubredditData(routine.SubredditName, routine.MaxPostsPerSync, (SortingEnum)routine.PostSorting);
                totalLinksFound = subredditLinks.Count;

                foreach (RedditPostMessage subredditDownloadLink in subredditLinks)
                {
                    subredditDownloadLink.ExecutionId = routineExecutionDTO.Id;
                    _queueService.Publish(subredditDownloadLink);
                }


                routineExecutionDTO.TotalLinksFound = totalLinksFound;
                routineExecutionDTO.Succeded = true;

                routineExecutionDTO = await _routineService.UpdateRoutineExecution(routineExecutionDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading queue. Message: " + ex.Message);
            }

        }
    }
}
