using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs
{
    public class RoutineExecutionFileDTO
    {
        public long RoutineExecutionId { get; set; }
        public string FileName { get; set; }
        public string SourceUrl { get; set; }
        public string? DownloadDirectory { get; set; }
        public int Classification { get; set; }
        public bool Succeded { get; set; }
    }
}
