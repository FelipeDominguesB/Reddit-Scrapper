using ContentDownloadWorker;
using RedditScrapper.Interface;
using RedditScrapper.Services.Plugins;
using RedditScrapper.Services;
using System.Net.Http;
using RedditScrapper.Model;
using RedditScrapper.Services.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedditScrapperService, RedditService>();
        services.AddSingleton<IWorkerService, ContentDownloadWorkerService>();
        services.AddSingleton<IDomainImageDownloader, ImgurImageDownloader>();
        services.AddSingleton<IDomainImageDownloader, RedditImageDownloader>();
        services.AddSingleton<IQueueService<SubredditDownloadLink>, SubredditPostQueueManagementService>();

        services.AddHttpClient<IRedditScrapperService, RedditService>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        });
    })
    .Build();

await host.RunAsync();
