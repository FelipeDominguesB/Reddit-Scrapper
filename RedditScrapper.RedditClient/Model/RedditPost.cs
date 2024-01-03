using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class RedditPost
    {

        public string kind { get; set; }

        public RedditPostData data { get; set; }
    }
}
