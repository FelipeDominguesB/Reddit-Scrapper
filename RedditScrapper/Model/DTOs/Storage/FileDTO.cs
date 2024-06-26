using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.DTOs.Storage
{
    public class FileDTO
    {
        public FileDTO() { }

        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
