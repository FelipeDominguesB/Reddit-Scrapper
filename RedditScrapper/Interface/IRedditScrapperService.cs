using RedditScrapper.Model;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IRedditScrapperService
    {
        public Task<ICollection<RedditPostMessage>> ReadSubredditData(string subredditName);
        public Task<ICollection<RedditPostMessage>> ReadSubredditData(string subredditName, int postCount, SortingEnum postSorting);
        public Task<bool> DownloadRedditPost(RedditPostMessage subredditDownloadLink);
        public Task<bool> DownloadRedditPostCollection(ICollection<RedditPostMessage> postCollection);
    }
}
