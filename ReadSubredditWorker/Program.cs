using ReadSubredditWorker;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<RedditService>();
        services.AddHttpClient<RedditService>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
