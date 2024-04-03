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
    .ConfigureAppConfiguration(config =>
    {
        config.AddEnvironmentVariables(prefix: "REDDITSCRAPPER_");

        IConfigurationRoot cfg = config.Build();
    })
    .ConfigureServices((hostContext, services) =>
    {


        services.AddHostedService<Worker>();

        services.AddSingleton<IWorkerService, ContentFetchWorkerService>();
        services.AddSingleton<IQueueService<RedditPostMessage>, SubredditPostQueueManagementService>();
        services.AddSingleton<IRedditScrapperService, RedditScrapperService>();
        services.AddSingleton<IRoutineService, RoutineService>();
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
