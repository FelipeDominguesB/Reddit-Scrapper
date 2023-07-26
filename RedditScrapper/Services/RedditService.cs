using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using RedditScrapper.Model.RedditResponses;

namespace RedditScrapper.Services
{
    public class RedditService
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;
        public RedditService(HttpClient httpClient, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
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


        public async Task<bool> DownloadSubredditData(List<SubredditDownloadLink> links)
        {
            List<IDomainImageDownloader> downloaders = _serviceProvider.GetServices<IDomainImageDownloader>().ToList();

            foreach(SubredditDownloadLink link in links) 
            {
                try
                {
                    IDomainImageDownloader? downloader = downloaders.FirstOrDefault(x => x.Id == link.domain);

                    if (downloader == null)
                        continue;

                    await downloader.DownloadLinkAsync(link);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
                
                
            }

            return true;
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
