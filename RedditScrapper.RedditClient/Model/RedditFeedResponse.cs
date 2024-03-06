using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RedditScrapper.RedditClient.Model
{
    public class RedditFeedResponse
    {
        public RedditFeedResponse() { }

        [JsonPropertyName("data")]
        public RedditFeedData Data { get; set; }

    }
}
