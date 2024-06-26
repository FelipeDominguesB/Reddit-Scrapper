﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedditScrapper.Context;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Enums;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient;
using RedditScrapper.RedditClient.Model;
using RedditScrapper.Services.Plugin;
using RedditScrapper.Services.Storage;

namespace RedditScrapper.Services.Scrapper
{
    public class RedditScrapperService : IRedditScrapperService
    {
        private readonly RedditHttpClient _redditClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;
        public RedditScrapperService(RedditHttpClient redditClient, IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _redditClient = redditClient;
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
        }

        public async Task<ICollection<RedditPostMessage>> ReadSubredditPosts(string subredditName)
        {
            List<RedditPostMessage> links = new List<RedditPostMessage>();
            int classification = 0;
            DateTime routineStartDate = DateTime.Now;
            string? after = string.Empty;

            for (int i = 0; i < 40; i++)
            {
                RedditFeedResponse redditFeedResponse = await _redditClient.ReadSubredditPage(subredditName, after);

                foreach (RedditPost post in redditFeedResponse.Data.Children)
                {
                    RedditPostMessage redditPostMessage = new RedditPostMessage();

                    redditPostMessage.Title = post.Data.Title;
                    redditPostMessage.Domain = post.Data.Domain;
                    redditPostMessage.SubredditName = post.Data.Subreddit;
                    redditPostMessage.Url = post.Data.UrlOverridenByDest;
                    redditPostMessage.Classification = ++classification;
                    redditPostMessage.RoutineDate = routineStartDate;

                    links.Add(redditPostMessage);

                }

                if (redditFeedResponse.Data.After == null)
                    break;

                after = redditFeedResponse.Data.After;
            }

            return links;
        }

        public async Task<ICollection<RedditPostMessage>> ReadSubredditPosts(string subredditName, int postCount, SortingEnum postSorting)
        {
            List<RedditPostMessage> links = new List<RedditPostMessage>();
            int classification = 0;
            DateTime routineStartDate = DateTime.Now;
            string? after = string.Empty;

            for (int i = 0; i < 40 && links.Count < postCount; i++)
            {
                RedditFeedResponse redditFeedResponse = await _redditClient.ReadSubredditPage(subredditName, GetSortingNameFromEnum(postSorting), after);
                foreach (RedditPost post in redditFeedResponse.Data.Children)
                {
                    classification++;

                    if (post.Data.IsGallery)
                        continue;

                    RedditPostMessage redditPostMessage = new RedditPostMessage();

                    redditPostMessage.Title = post.Data.Title;
                    redditPostMessage.Domain = post.Data.Domain;
                    redditPostMessage.SubredditName = post.Data.Subreddit;
                    redditPostMessage.Url = post.Data.UrlOverridenByDest;
                    redditPostMessage.Classification = classification;
                    redditPostMessage.RoutineDate = routineStartDate;

                    links.Add(redditPostMessage);

                    if (links.Count >= postCount)
                        break;
                }

                if (redditFeedResponse.Data.After == null)
                    break;

                after = redditFeedResponse.Data.After;
            }

            return links;
        }

        public async Task<RoutineExecutionFileDTO> DownloadRedditPost(RedditPostMessage subredditDownloadLink)
        {
            RoutineExecutionFileDTO routineExecutionFile = new RoutineExecutionFileDTO();

            List<IDomainImageDownloaderPlugin> downloaders = _serviceProvider.GetServices<IDomainImageDownloaderPlugin>().ToList();

            IDomainImageDownloaderPlugin? downloader = downloaders.FirstOrDefault(x => x.Id.Contains(subredditDownloadLink.Domain));

            if (downloader == null)
                throw new Exception($"Downloader not found for {subredditDownloadLink.Domain} domain");

            return await downloader.DownloadMedia(subredditDownloadLink);

        }

        public async Task<bool> DownloadRedditPostCollection(ICollection<RedditPostMessage> links)
        {
            List<IDomainImageDownloaderPlugin> downloaders = _serviceProvider.GetServices<IDomainImageDownloaderPlugin>().ToList();


            foreach (RedditPostMessage link in links)
            {
                try
                {
                    IDomainImageDownloaderPlugin? downloader = downloaders.FirstOrDefault(x => x.Id == link.Domain);

                    if (downloader == null)
                        continue;

                    await downloader.DownloadMedia(link);
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
