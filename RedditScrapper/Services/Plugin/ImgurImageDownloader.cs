using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using RedditScrapper.Services.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Plugin
{
    public class ImgurImageDownloader : IDomainImageDownloaderPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string basePath;
        private readonly IStorageFacade _storageFacade;
        public string Id { get; set; } = "i.imgur.com";

        public ImgurImageDownloader(IStorageFacade storageFacade)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RedditScrapper-PC");
            _storageFacade = storageFacade;
        }

        public async Task<RoutineExecutionFileDTO> DownloadMedia(RedditPostMessage downloadObject)
        {
            string path = $"{basePath}\\{downloadObject.RoutineDate.ToString("MM-dd")}\\{downloadObject.SubredditName}";
            string fileName = $"{downloadObject.Classification}-{downloadObject.Url.Split("/").Last()}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            if (downloadObject.Url.Contains(".gifv"))
            {
                HttpResponseMessage pageResponse = await _httpClient.GetAsync(downloadObject.Url);

                if (pageResponse.Content.Headers.ContentLength == 503)
                    throw new Exception();

                string htmlCode = await pageResponse.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);

                HtmlNode? node = doc.DocumentNode.Descendants("meta")
                            .Where(n =>
                                n.HasAttributes
                                && n.Attributes.Any(x => x.Name.Contains("property"))
                                && (n.Attributes["property"].Value == "og:video" || n.Attributes["property"].Value == "og:image")
                                && !n.Attributes["content"].Value.Contains("?play"))
                            .FirstOrDefault();

                if (node == null)
                    throw new Exception();

                downloadObject.Url = node.Attributes["content"].Value;

                fileName = fileName.Replace(".gifv", node.Attributes["property"].Value == "og:video" ? ".mp4" : ".jpg");
            }

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.Url);

            if (response.Content.Headers.ContentLength == 503)
                throw new Exception();

            FileDTO fileDTO = await _storageFacade.WriteFile(response.Content.ReadAsStream(), fileName, downloadObject);
            
            RoutineExecutionFileDTO result = new RoutineExecutionFileDTO()
            {
                Classification = downloadObject.Classification,
                DownloadDirectory = fileDTO.FilePath,
                SourceUrl = downloadObject.Url,
                RoutineExecutionId = downloadObject.ExecutionId,
                FileName = fileDTO.FileName,
                Succeded = true
            };

            return result;
        }

    }
}
