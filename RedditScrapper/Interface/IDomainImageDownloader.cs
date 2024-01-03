using RedditScrapper.Model;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IDomainImageDownloader
    {
        public string Id { get; set;  }
        Task<bool> DownloadLinkAsync(RedditPostMessage downloadObject);
    }
}
