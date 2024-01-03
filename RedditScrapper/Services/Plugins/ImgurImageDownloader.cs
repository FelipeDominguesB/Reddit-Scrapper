using HtmlAgilityPack;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using RedditScrapper.Model.Message;
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

        public async Task<bool> DownloadLinkAsync(RedditPostMessage downloadObject)
        {

            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.RoutineDate.ToString("MM-dd")}\\{downloadObject.SubredditName}";
            string fileName = $"{downloadObject.Classification}-{downloadObject.Url.Split("/").Last()}";
            string filePath = $"{path}\\{fileName}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            if (downloadObject.Url.Contains(".gifv"))
            {
                HttpResponseMessage pageResponse = await _httpClient.GetAsync(downloadObject.Url);

                if (pageResponse.Content.Headers.ContentLength == 503)
                    return false;

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
                    return false;
                
                downloadObject.Url = node.Attributes["content"].Value;

                filePath = filePath.Replace(".gifv", node.Attributes["property"].Value == "og:video" ? ".mp4" : ".jpg");
            }

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.Url);

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
