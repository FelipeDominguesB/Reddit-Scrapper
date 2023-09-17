using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model
{
    public class SubredditDownloadLink
    {
        public string subredditName { get; set; }
        public string url { get; set; }
        public string domain { get; set; }
        public int classification { get; set; }
        public string title { get; set; }

        public DateTime routineDate { get; set; }

    }
}
