using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Storage
{
    public interface IStorageFacade
    {
        public Task<FileDTO> WriteFile(Stream stream, string fileName, RedditPostMessage? redditPostMessage);
        public Task<Stream> ReadFile(string filePath, string? fileName);
    }
}
