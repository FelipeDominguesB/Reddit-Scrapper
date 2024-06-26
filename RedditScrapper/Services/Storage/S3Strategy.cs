using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Storage
{
    public class S3Strategy : IStorageStrategy
    {
        public string Name { get; } = "S3";

        public async Task<FileDTO> WriteFile(Stream stream, string fileName, RedditPostMessage? redditPostMessage)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }
    }
}
