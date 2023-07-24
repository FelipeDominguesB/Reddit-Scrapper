using System;

namespace RedditScrapper // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static readonly HttpClient httpClient = new HttpClient();


        static void Main(string[] args)
        {

            var result = FetchFromReddit().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }


        public static async Task<object> FetchFromReddit()
        {


            httpClient.BaseAddress = new Uri("https://www.reddit.com/");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.29.2");
            
            var result = await httpClient.GetAsync("/r/ass/top/.json?t=all");

            return result;
        }


        public static async Task<object>
    }
}