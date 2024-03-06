using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class SubredditDownloadLink
    {
        public SubredditDownloadLink() { }

        [JsonPropertyName("subredditName")]
        public string SubredditName { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("domain")]
        public string Domain { get; set; }
        
        [JsonPropertyName("classification")]
        public int Classification { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("routineDate")]
        public DateTime RoutineDate { get; set; }

    }
}
