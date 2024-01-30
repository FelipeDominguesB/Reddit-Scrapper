using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy.Model;
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

        private readonly IQueueService<RedditPostMessage> _queueService;

        public ContentFetchWorkerService(IRedditScrapperService redditService, IQueueService<RedditPostMessage> queueService, IRoutineService routineService)
        {
            _routineService = routineService;
            _redditService = redditService;
            _queueService = queueService;
        }

        public async Task<bool> Start()
        {

            ICollection<Routine> pendingRoutines = await _routineService.GetPendingRoutines();

            foreach (Routine routine in pendingRoutines)
                await RunRoutine(routine);

            return true;
        }

        private async Task RunRoutine(Routine routine)
        {

            bool isSuccessful = false;
            int totalLinksFound = 0;
            try
            {
                
                ICollection<RedditPostMessage> subredditLinks = await _redditService.ReadSubredditData(routine.SubredditName, routine.MaxPostsPerSync, (SortingEnum)routine.PostSorting);
                totalLinksFound = subredditLinks.Count;

                foreach (RedditPostMessage subredditDownloadLink in subredditLinks)
                    _queueService.Publish(subredditDownloadLink);

                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading queue. Message: " + ex.Message);

            }
            finally
            {
                RoutineExecutionDTO routineExecutionDTO = new RoutineExecutionDTO()
                {
                    Id = routine.Id,
                    Succeded = isSuccessful,
                    RoutineId = routine.Id,
                    MaxPostsPerSync = routine.MaxPostsPerSync,
                    PostSorting = routine.PostSorting,
                    SyncRate = routine.SyncRate,
                    TotalLinksFound = totalLinksFound
                };

                await _routineService.AddRoutineExecution(routineExecutionDTO);
            }
        }


        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}
