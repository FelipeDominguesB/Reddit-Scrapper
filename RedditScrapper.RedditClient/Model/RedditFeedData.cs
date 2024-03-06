using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class RedditFeedData
    {

        public RedditFeedData() { }

        [JsonPropertyName("after")]
        public string After { get; set; }
        
        [JsonPropertyName("before")]
        public string Before { get; set; }
        
        [JsonPropertyName("children")]
        public RedditPost[] Children { get; set; }
        
        [JsonPropertyName("dist")]
        public int Dist { get; set; }
    }
}
