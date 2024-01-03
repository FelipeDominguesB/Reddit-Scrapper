using Newtonsoft.Json;
using RedditScrapper.RedditProxy.Model;

namespace RedditScrapper.RedditProxy
{
    public class RedditClient
    {

        private HttpClient _httpClient;
        public RedditClient(HttpClient httpClient) { 
            _httpClient = httpClient;
            
        }

        public async Task<RedditFeedResponse> ReadSubredditPage(string subredditName, string sorting = "all", string? after = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/r/{subredditName}/top/.json?t={sorting}&after={after}");

            string responseText = await response.Content.ReadAsStringAsync();

            RedditFeedResponse redditFeedResponse = JsonConvert.DeserializeObject<RedditFeedResponse>(responseText);

            return redditFeedResponse;

        }

    }
}
