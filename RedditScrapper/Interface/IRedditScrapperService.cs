using RedditScrapper.Model;
using RedditScrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IRedditScrapperService
    {
        public Task<ICollection<SubredditDownloadLink>> ReadSubredditData(string subredditName);
        public Task<ICollection<SubredditDownloadLink>> ReadSubredditData(string subredditName, int postCount, SortingEnum postSorting);
        public Task<bool> DownloadRedditPost(SubredditDownloadLink subredditDownloadLink);
        public Task<bool> DownloadRedditPostCollection(ICollection<SubredditDownloadLink> postCollection);
    }
}
