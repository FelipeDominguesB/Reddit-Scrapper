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
using RedditScrapper.RedditProxy;
using RedditScrapper.Model.Message;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IWorkerService, ReadSubredditWorkerService>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IRoutineService, RoutineService>();

        services.AddDbContext<RedditScrapperContext>(
            options => options.UseSqlServer("Server=localhost;Database=RedditScrapper;Trusted_Connection=True;Encrypt=false;")
        );

        services.AddHttpClient<RedditClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
