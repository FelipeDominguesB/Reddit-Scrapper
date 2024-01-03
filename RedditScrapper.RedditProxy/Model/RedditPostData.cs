using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.RedditProxy.Model
{
    public class RedditPostData
    {
        public string subreddit { get; set; }
        public string title { get; set; }
        public string domain { get; set; }
        public string url_overridden_by_dest { get; set; }

    }
}
