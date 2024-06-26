using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Model.DTOs.Storage;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Storage
{
    public class StorageFacade : IStorageFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public StorageFacade(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }


        public Task<Stream> ReadFile(string filePath, string? fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<FileDTO> WriteFile(Stream stream, string fileName, RedditPostMessage? redditPostMessage)
        {
            IStorageStrategy storageStrategy = _serviceProvider.GetServices<IStorageStrategy>().First(x => x.Name == "FS");
            return await storageStrategy.WriteFile(stream, fileName, redditPostMessage);
        }
    }
}
