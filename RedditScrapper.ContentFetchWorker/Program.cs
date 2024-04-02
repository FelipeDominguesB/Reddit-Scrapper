using RedditScrapper.ContentFetchWorker;
using RedditScrapper.Context;
using Microsoft.EntityFrameworkCore;
using RedditScrapper.RedditClient;
using RedditScrapper.Model.Message;
using RedditScrapper.Services.Worker;
using RedditScrapper.Services.Scrapper;
using RedditScrapper.Services.Queue;
using RedditScrapper.Services.Routines;
using RedditScrapper.Mapper;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IWorkerService, ContentFetchWorkerService>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IRoutineService, RoutineService>();
        services.AddAutoMapper(typeof(RoutineProfile));

        services.AddDbContext<RedditScrapperContext>(
            options => options.UseSqlServer("Server=localhost;Database=RedditScrapper;Trusted_Connection=False;Encrypt=false; User Id=sa;Password=Pass@word1")
        );

        services.AddHttpClient<RedditHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
