﻿using HtmlAgilityPack;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Plugins
{
    public class ImgurImageDownloader : IDomainImageDownloader
    {
        private readonly HttpClient _httpClient;
        public string Id { get; set; } = "i.imgur.com";

        public ImgurImageDownloader()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");

        }

        public async Task<bool> DownloadLinkAsync(SubredditDownloadLink downloadObject)
        {

            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.subredditName}";
            string fileName = $"{downloadObject.classification}-{downloadObject.url.Split("/").Last()}";
            string filePath = $"{path}\\{fileName}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            if (downloadObject.url.Contains(".gifv"))
            {
                HttpResponseMessage pageResponse = await _httpClient.GetAsync(downloadObject.url);

                if (pageResponse.Content.Headers.ContentLength == 503)
                    return false;

                string htmlCode = await pageResponse.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);

                HtmlNode? node = doc.DocumentNode.Descendants("meta")
                            .Where(n =>
                                n.HasAttributes
                                && n.Attributes.Any(x => x.Name.Contains("property"))
                                && (n.Attributes["property"].Value == "og:video" || n.Attributes["property"].Value == "og:image"))
                            .FirstOrDefault();

                if (node != null)
                    downloadObject.url = node.Attributes["content"].Value;
            }

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.url);

            if (response.Content.Headers.ContentLength == 503)
                return false;

            using (var filestream = File.Create(filePath))
            {
                var stream = response.Content.ReadAsStream();
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(filestream);
            }

            return true;
        }

    }
}
