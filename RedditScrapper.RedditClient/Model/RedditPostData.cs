using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class RedditPostData
    {
        public RedditPostData() { }

        [JsonPropertyName("subreddit")]
        public string Subreddit { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }
        
        [JsonPropertyName("url_overridden_by_dest")]
        public string UrlOverridenByDest { get; set; }

    }
}
