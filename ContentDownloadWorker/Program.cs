using ContentDownloadWorker;
using RedditScrapper.Interface;
using RedditScrapper.Services.Plugins;
using RedditScrapper.Services;
using System.Net.Http;
using RedditScrapper.Model;
using RedditScrapper.Services.Workers;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IWorkerService, ContentDownloadWorkerService>();
        services.AddSingleton<IDomainImageDownloader, ImgurImageDownloader>();
        services.AddSingleton<IDomainImageDownloader, RedgifsImageDownloader>();
        services.AddSingleton<IDomainImageDownloader, RedditImageDownloader>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();

        services.AddHttpClient<RedditClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        });
    })
    .Build();

await host.RunAsync();
