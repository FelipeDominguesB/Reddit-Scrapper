using HtmlAgilityPack;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient.Model;


namespace RedditScrapper.Services.Plugin
{
    public class RedgifsImageDownloader : IDomainImageDownloaderPlugin
    {
        private readonly HttpClient _httpClient;
        public string Id { get; set; } = "redgifs.com";

        public RedgifsImageDownloader()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        }

        public async Task<RoutineExecutionFileDTO> DownloadLinkAsync(RedditPostMessage downloadObject)
        {


            if (downloadObject.Url.Split('.').Last().Contains("com/"))
            {

                HttpResponseMessage pageResponse = await _httpClient.GetAsync(downloadObject.Url);
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
                    throw new Exception();

                downloadObject.Url = node.Attributes["content"].Value;
            }

            string path = $"D:\\DUMP\\Scrapper\\{downloadObject.RoutineDate.ToString("MM-dd")}\\{downloadObject.SubredditName}";

            bool exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            string fileName = $"{downloadObject.Classification} - {downloadObject.Url.Split("/").Last()}";

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.Url);

            if (response.Content.Headers.ContentLength == 503)
                throw new Exception();

            using (var filestream = File.Create($"{path}\\{fileName}"))
            {
                var stream = response.Content.ReadAsStream();
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(filestream);
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
