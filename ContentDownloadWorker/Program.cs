using ContentDownloadWorker;
using System.Net.Http;
using RedditScrapper.Model;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditProxy;
using RedditScrapper.Services.Worker;
using RedditScrapper.Services.Scrapper;
using RedditScrapper.Services.Plugin;
using RedditScrapper.Services.Queue;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IWorkerService, ContentDownloadWorkerService>();
        services.AddSingleton<IDomainImageDownloaderPlugin, ImgurImageDownloader>();
        services.AddSingleton<IDomainImageDownloaderPlugin, RedgifsImageDownloader>();
        services.AddSingleton<IDomainImageDownloaderPlugin, RedditImageDownloader>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();

        services.AddHttpClient<RedditClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        });
    })
    .Build();

await host.RunAsync();
