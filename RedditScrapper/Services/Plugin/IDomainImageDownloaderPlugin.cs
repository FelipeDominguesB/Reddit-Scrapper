using RedditScrapper.Model;
using RedditScrapper.Model.DTOs;
using RedditScrapper.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Plugin
{
    public interface IDomainImageDownloaderPlugin
    {
        public string Id { get; set; }
        Task<RoutineExecutionFileDTO> DownloadLinkAsync(RedditPostMessage downloadObject);
    }
}
