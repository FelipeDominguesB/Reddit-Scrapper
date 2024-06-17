using RedditScrapper.ContentDownloadWorker;
using System.Net.Http;
using RedditScrapper.Model;
using RedditScrapper.Model.Message;
using RedditScrapper.RedditClient;
using RedditScrapper.Services.Scrapper;
using RedditScrapper.Services.Plugin;
using RedditScrapper.Services.Queue;
using RedditScrapper.Mapper;
using RedditScrapper.Services.Routines;
using Microsoft.EntityFrameworkCore;
using RedditScrapper.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddEnvironmentVariables(prefix: "REDDITSCRAPPER_");

        IConfigurationRoot cfg = config.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IDomainImageDownloaderPlugin, ImgurImageDownloader>();
        services.AddSingleton<IDomainImageDownloaderPlugin, RedditImageDownloader>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRoutineManagementService, RoutineManagementService>();
        services.AddAutoMapper(typeof(RoutineProfile));


        services.AddDbContext<RedditScrapperContext>(options => options.UseSqlServer(hostContext.Configuration.GetValue<string>("CONNECTIONSTRING")));

        services.AddHttpClient<RedditHttpClient>(client =>
        {
            client.BaseAddress = new Uri(hostContext.Configuration.GetSection("REDDIT:URL").Value);
            client.DefaultRequestHeaders.Add("User-Agent", hostContext.Configuration.GetSection("REDDIT:DEFAULTAGENT").Value);
        });

    })
    .Build();

await host.RunAsync();
