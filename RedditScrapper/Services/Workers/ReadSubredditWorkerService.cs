using RedditScrapper.Domain.Entities;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using RedditScrapper.Model.Enums;
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
        private readonly IRoutineService _routineService;

        private readonly IQueueService<SubredditDownloadLink> _queueService;

        public ReadSubredditWorkerService(IRedditScrapperService redditService, IQueueService<SubredditDownloadLink> queueService, IRoutineService routineService)
        {
            _routineService = routineService;
            _redditService = redditService;
            _queueService = queueService;
        }

        public async Task<bool> Start()
        {

            List<SyncRoutine> pendingRoutines = await _routineService.GetPendingRoutines();

            foreach(SyncRoutine routine in pendingRoutines) 
                await this.RunRoutine(routine);

            return true;
        }

        private async Task RunRoutine(SyncRoutine routine)
        {
            bool isSuccessful = false;
            
            try
            { 
                ICollection<SubredditDownloadLink> subredditLinks = await _redditService.ReadSubredditData(routine.SubredditName, routine.MaxPostsPerSync, (SortingEnum) routine.PostSorting);

                foreach (SubredditDownloadLink subredditDownloadLink in subredditLinks)
                    _queueService.Publish(subredditDownloadLink);

                isSuccessful = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception reading queue. Message: " + ex.Message);

            }
            finally
            {
                await this._routineService.AddHistoryToRoutine(routine.Id, isSuccessful);
            }
        }


        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}
