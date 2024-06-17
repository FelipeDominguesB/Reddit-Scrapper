using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Scrapper
{
    public interface IRedditScrapperService
    {
        public Task<ICollection<RedditPostMessage>> ReadSubredditPosts(string subredditName);
        public Task<ICollection<RedditPostMessage>> ReadSubredditPosts(string subredditName, int postCount, SortingEnum postSorting);
        public Task<RoutineExecutionFileDTO> DownloadRedditPost(RedditPostMessage subredditDownloadLink);
        public Task<bool> DownloadRedditPostCollection(ICollection<RedditPostMessage> postCollection);
    }
}
