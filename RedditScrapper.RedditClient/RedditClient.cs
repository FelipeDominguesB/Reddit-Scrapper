using RedditScrapper.RedditClient.Model;
using System.Text.Json;

namespace RedditScrapper.RedditClient
{
    public class RedditHttpClient
    {

        private HttpClient _httpClient;
        public RedditHttpClient(HttpClient httpClient) { 
            _httpClient = httpClient;
            
        }

        public async Task<RedditFeedResponse> ReadSubredditPage(string subredditName, string sorting = "all", string? after = null)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/r/{subredditName}/top/.json?t={sorting}&after={after}");

            string responseText = await response.Content.ReadAsStringAsync();

            RedditFeedResponse redditFeedResponse = JsonSerializer.Deserialize<RedditFeedResponse>(responseText);

            return redditFeedResponse;

        }

    }
}
