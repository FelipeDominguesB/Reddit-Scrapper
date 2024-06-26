using Microsoft.Extensions.Configuration;
using RedditScrapper.Model;
using RedditScrapper.Model.DTOs.Routine;
using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using RedditScrapper.Services.Storage;

namespace RedditScrapper.Services.Plugin
{
    public class RedditImageDownloader : IDomainImageDownloaderPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string basePath;
        private readonly IStorageFacade _storageFacade;
        public string Id { get; set; } = "i.redd.it";

        public RedditImageDownloader(IConfiguration configuration, IStorageFacade storageFacade)
        {
            _httpClient = new HttpClient();
            basePath = configuration.GetSection("DOWNLOADPATH").Value;
            _storageFacade = storageFacade;
        }

        public async Task<RoutineExecutionFileDTO> DownloadMedia(RedditPostMessage downloadObject)
        {
            string fileName = $"{downloadObject.Classification}-{downloadObject.Url.Split("/").Last()}";

            HttpResponseMessage response = await _httpClient.GetAsync(downloadObject.Url);

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
