using RedditScrapper.Interface;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services
{
    public class RedditImageDownloader : IDomainImageDownloader
    {
        private readonly HttpClient _httpClient;
        public string Id { get; set; } = "i.redd.it";

        public RedditImageDownloader()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> DownloadLinkAsync(SubredditDownloadLink downloadObject)
        {



            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.subredditName}";

            bool exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);

            string fileName = $"{downloadObject.classification}-{downloadObject.url.Split("/").Last()}";

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.url);


            using (var filestream = File.Create($"{path}\\{fileName}"))
            {
                var stream = response.Content.ReadAsStream();
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(filestream);
            }


            return true;
        }



        public Task<object> ReadWebPage()
        {
            return null;
        }
    }
}
