﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.RedditResponses;

namespace RedditScrapper.Services
{
    public class RedditScrapperService : IRedditScrapperService
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;
        public RedditScrapperService(HttpClient httpClient, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
        }

        
        public async Task<ICollection<SubredditDownloadLink>> ReadSubredditData(string subredditName)
        {
            List<SubredditDownloadLink> links = new List<SubredditDownloadLink>();
            int classification = 0;
            DateTime routineStartDate = DateTime.Now;
            string? after = String.Empty;

            for (int i = 0; i <40; i++)
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
                    subredditDownloadLink.routineDate = routineStartDate;
                    links.Add(subredditDownloadLink);
                }

                if (redditFeedResponse.data.after == null)
                    break;

                after = redditFeedResponse.data.after;
            }

            return links;
        }

        public async Task<ICollection<SubredditDownloadLink>> ReadSubredditData(string subredditName, int postCount, SortingEnum postSorting)
        {
            List<SubredditDownloadLink> links = new List<SubredditDownloadLink>();
            int classification = 0;
            DateTime routineStartDate = DateTime.Now;
            string? after = String.Empty;

            for (int i = 0; i < 40 && links.Count < postCount ; i++)
            {
                RedditFeedResponse redditFeedResponse = await ReadSubredditPage(subredditName, this.GetSortingNameFromEnum(postSorting), after);

                foreach (RedditPost post in redditFeedResponse.data.children)
                {
                    SubredditDownloadLink subredditDownloadLink = new SubredditDownloadLink();
                    subredditDownloadLink.title = post.data.title;
                    subredditDownloadLink.domain = post.data.domain;
                    subredditDownloadLink.subredditName = post.data.subreddit;
                    subredditDownloadLink.url = post.data.url_overridden_by_dest;
                    subredditDownloadLink.classification = ++classification;
                    subredditDownloadLink.routineDate = routineStartDate;
                    
                    links.Add(subredditDownloadLink);

                    if (links.Count >= postCount)
                        break;
                }

                if (redditFeedResponse.data.after == null)
                    break;

                after = redditFeedResponse.data.after;
            }

            return links;
        }

        public async Task<bool> DownloadRedditPost(SubredditDownloadLink subredditDownloadLink)
        {
            List<IDomainImageDownloader> downloaders = _serviceProvider.GetServices<IDomainImageDownloader>().ToList();

            try
            {
                IDomainImageDownloader? downloader = downloaders.FirstOrDefault(x => subredditDownloadLink.domain.Contains(x.Id));

                if (downloader == null)
                    return false;

                await downloader.DownloadLinkAsync(subredditDownloadLink);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("Finished downloading " + subredditDownloadLink.classification);
            }
            return true;
        }

        public async Task<bool> DownloadRedditPostCollection(ICollection<SubredditDownloadLink> links)
        {
            List<IDomainImageDownloader> downloaders = _serviceProvider.GetServices<IDomainImageDownloader>().ToList();


            foreach (SubredditDownloadLink link in links)
            {
                try
                {
                    IDomainImageDownloader? downloader = downloaders.FirstOrDefault(x => x.Id == link.domain);

                    if (downloader == null)
                        continue;

                    await downloader.DownloadLinkAsync(link);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return true;
        }
        private async Task<RedditFeedResponse> ReadSubredditPage(string subredditName, string sorting = "all", string? after = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/r/{subredditName}/top/.json?t={sorting}&after={after}");

            string responseText = await response.Content.ReadAsStringAsync();

            RedditFeedResponse redditFeedResponse = JsonConvert.DeserializeObject<RedditFeedResponse>(responseText);

            return redditFeedResponse;

        }

        private string GetSortingNameFromEnum(SortingEnum sortingEnum)
        {
            string sortingName = string.Empty;


            switch (sortingEnum)
            {
                case SortingEnum.Daily:
                    sortingName = "day";
                    break;
                case SortingEnum.Weekly:
                    sortingName = "week";
                    break;
                case SortingEnum.Monthly:
                    sortingName = "month";
                    break;
                case SortingEnum.Yearly:
                    sortingName = "year";
                    break;
                case SortingEnum.All:
                    sortingName = "all";
                    break;
            }

            return sortingName;
        }

       
    }
}