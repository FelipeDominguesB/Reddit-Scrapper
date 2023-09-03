using ReadSubredditWorker;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Services;
using RedditScrapper.Interface;
using RedditScrapper.Services.Plugins;
using RedditScrapper.Model;
using RedditScrapper.Services.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IWorkerService, ReadSubredditWorkerService>();
        services.AddSingleton<IQueueService<SubredditDownloadLink>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRedditScrapperService, RedditService>();
        
        services.AddHttpClient<IRedditScrapperService, RedditService>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
