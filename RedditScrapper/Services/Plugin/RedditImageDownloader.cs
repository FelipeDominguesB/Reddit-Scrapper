using RedditScrapper.Model;
using RedditScrapper.Model.DTOs;
using RedditScrapper.Model.Message;

namespace RedditScrapper.Services.Plugin
{
    public class RedditImageDownloader : IDomainImageDownloaderPlugin
    {
        private readonly HttpClient _httpClient;
        public string Id { get; set; } = "i.redd.it";

        public RedditImageDownloader()
        {
            _httpClient = new HttpClient();
        }

        public async Task<RoutineExecutionFileDTO> DownloadLinkAsync(RedditPostMessage downloadObject)
        {
            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.RoutineDate.ToString("MM-dd")}\\{downloadObject.SubredditName}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            string fileName = $"{downloadObject.Classification}-{downloadObject.Url.Split("/").Last()}";

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.Url);

            using (var filestream = File.Create($"{path}\\{fileName}"))
            {
                var stream = response.Content.ReadAsStream();
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(filestream);
            }

            RoutineExecutionFileDTO result = new RoutineExecutionFileDTO()
            {
                Classification = downloadObject.Classification,
                DownloadDirectory = path,
                SourceUrl = downloadObject.Url,
                RoutineExecutionId = downloadObject.ExecutionId,
                FileName = fileName,
                Succeded = true
            };

            return result;
        }



        public Task<object> ReadWebPage()
        {
            return null;
        }
    }
}
