using Microsoft.Extensions.Configuration;
using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Storage
{
    public class FileSystemStrategy : IStorageStrategy
    {

        private readonly string basePath = string.Empty;
        public FileSystemStrategy(IConfiguration configuration)
        {
            basePath = configuration.GetSection("DOWNLOADPATH").Value;
        }

        public string Name { get; } = "FS";

        public async Task<FileDTO> WriteFile(Stream stream, string fileName, RedditPostMessage? redditPostMessage)
        {
            string path = $"{basePath}\\{redditPostMessage?.RoutineDate.ToString("MM-dd")}\\{redditPostMessage?.SubredditName}";


            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using FileStream filestream = File.Create($"{path.TrimEnd('/')}/{fileName}");
            
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(filestream);


            return new FileDTO() 
            { 
                FilePath = path,
                FileName = fileName
            };
        }
    }
}
