using RedditScrapper.ContentDownloadWorker;
using System.Net.Http;
using RedditScrapper.Model;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient;
using RedditScrapper.Services.Worker;
using RedditScrapper.Services.Scrapper;
using RedditScrapper.Services.Plugin;
using RedditScrapper.Services.Queue;
using RedditScrapper.Mapper;
using RedditScrapper.Services.Routines;
using Microsoft.EntityFrameworkCore;
using RedditScrapper.Context;

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
        services.AddSingleton<IRoutineService, RoutineService>();
        services.AddAutoMapper(typeof(RoutineProfile));


        services.AddDbContext<RedditScrapperContext>(
            options => options.UseSqlServer("Server=localhost;Database=RedditScrapper;Trusted_Connection=True;Encrypt=false; User Id=sa;Password=<YourStrong@Passw0rd>")
        );

        services.AddHttpClient<RedditHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC");
        });
    })
    .Build();

await host.RunAsync();
