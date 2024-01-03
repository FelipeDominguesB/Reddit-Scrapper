using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class RedditFeedData
    {

        public RedditFeedData() { }

        public string after { get; set; }
        public string before { get; set; }
        public RedditPost[] children { get; set; }
        public int dist { get; set; }
    }
}
