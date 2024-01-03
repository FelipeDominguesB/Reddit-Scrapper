using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Model.Message
{
    public class RedditPostMessage
    {
        public string SubredditName { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public int Classification { get; set; }
        public string Title { get; set; }

        public DateTime RoutineDate { get; set; }
    }
}
