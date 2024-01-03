using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
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
    public class ReadSubredditWorkerService : IWorkerService
    {

        private readonly IRedditScrapperService _redditService;
        private readonly IRoutineService _routineService;

        private readonly IQueueService<RedditPostMessage> _queueService;

        public ReadSubredditWorkerService(IRedditScrapperService redditService, IQueueService<RedditPostMessage> queueService, IRoutineService routineService)
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

            try
            {
                ICollection<RedditPostMessage> subredditLinks = await _redditService.ReadSubredditData(routine.SubredditName, routine.MaxPostsPerSync, (SortingEnum)routine.PostSorting);

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
                await _routineService.AddHistoryToRoutine(routine.Id, isSuccessful);
            }
        }


        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}
