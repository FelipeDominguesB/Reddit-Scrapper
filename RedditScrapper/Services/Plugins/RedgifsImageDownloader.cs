using HtmlAgilityPack;
using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Plugins
{
    public class RedgifsImageDownloader : IDomainImageDownloader
    {
        private readonly HttpClient _httpClient;
        public string Id { get; set; } = "redgifs.com";

        public RedgifsImageDownloader()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        }

        public async Task<bool> DownloadLinkAsync(SubredditDownloadLink downloadObject)
        {


            if (downloadObject.url.Split('.').Last().Contains("com/"))
            {

                HttpResponseMessage pageResponse = await _httpClient.GetAsync(downloadObject.url);
                string htmlCode = await pageResponse.Content.ReadAsStringAsync();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlCode);

                HtmlNode? node = doc.DocumentNode.Descendants("meta")
                            .Where(n =>
                                n.HasAttributes
                                && n.Attributes.Any(x => x.Name.Contains("property"))
                                && (n.Attributes["property"].Value == "og:video" || n.Attributes["property"].Value == "og:image"))
                            .FirstOrDefault();

                if (node == null)
                    return false;

                downloadObject.url = node.Attributes["content"].Value;
            }

            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.subredditName}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            string fileName = $"{downloadObject.classification}-{downloadObject.url.Split("/").Last()}";

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.url);

            if (response.Content.Headers.ContentLength == 503)
                return false;

            using (var filestream = File.Create($"{path}\\{fileName}"))
            {
                var stream = response.Content.ReadAsStream();
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(filestream);
            }


            return true;
        }



        public Task<object> ReadWebPage()
        {
            return null;
        }
    }
}
