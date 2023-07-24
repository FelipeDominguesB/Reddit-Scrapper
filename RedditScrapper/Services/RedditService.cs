using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedditScrapper.Model;
using RedditScrapper.Model.RedditResponses;

namespace RedditScrapper.Services
{
    public class RedditService
    {
        private readonly HttpClient _httpClient;

        public RedditService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<List<SubredditDownloadLink>> ReadSubredditData(string subredditName)
        {
            List<SubredditDownloadLink> links = new List<SubredditDownloadLink>();
            int classification = 0;

            string? after = String.Empty;

            for (int i = 0; i < 10; i++)
            {
                RedditFeedResponse redditFeedResponse = await ReadSubredditPage(subredditName, after);

                foreach(RedditPost post in redditFeedResponse.data.children)
                {
                    SubredditDownloadLink subredditDownloadLink = new SubredditDownloadLink();
                    subredditDownloadLink.title = post.data.title;
                    subredditDownloadLink.domain = post.data.domain;
                    subredditDownloadLink.subredditName = post.data.subreddit;
                    subredditDownloadLink.url = post.data.url_overridden_by_dest;
                    subredditDownloadLink.classification = ++classification;

                    links.Add(subredditDownloadLink);
                }

                if (redditFeedResponse.data.after == null)
                    break;

                after = redditFeedResponse.data.after;
            }

            return links;
        }


        public async Task<RedditFeedResponse> ReadSubredditPage(string subredditName, string? after = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/r/{subredditName}/top/.json?t=all&after={after}");

            string responseText = await response.Content.ReadAsStringAsync();

            RedditFeedResponse redditFeedResponse = JsonConvert.DeserializeObject<RedditFeedResponse>(responseText);


            return redditFeedResponse;

        }
    }
}
