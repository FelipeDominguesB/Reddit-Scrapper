using ReadSubredditWorker;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Services;
using RedditScrapper.Interface;
using RedditScrapper.Services.Plugins;
using RedditScrapper.Model;
using RedditScrapper.Services.Workers;
using RedditScrapper.Context;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IWorkerService, ReadSubredditWorkerService>();
        services.AddSingleton<IQueueService<SubredditDownloadLink>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IRoutineService, RoutineService>();

        services.AddDbContext<RedditScrapperContext>(
            options => options.UseSqlServer("Server=localhost;Database=RedditScrapper;Trusted_Connection=True;Encrypt=false;")
        );

        services.AddHttpClient<IRedditScrapperService, RedditScrapperService>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
