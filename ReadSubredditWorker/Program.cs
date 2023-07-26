using ReadSubredditWorker;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Services;
using RedditScrapper.Interface;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<RedditService>();

        services.AddSingleton<IDomainImageDownloader, ImgurImageDownloader>();
        services.AddSingleton<IDomainImageDownloader, RedditImageDownloader>();
        services.AddHttpClient<RedditService>(client =>
        {
            client.BaseAddress = new Uri("https://www.reddit.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Felipe-PC"); 
        });
    })
    .Build();

await host.RunAsync();
