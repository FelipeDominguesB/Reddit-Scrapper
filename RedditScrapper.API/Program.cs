using Microsoft.EntityFrameworkCore;
using RedditScrapper.Context;
using RedditScrapper.Mapper;
using RedditScrapper.Services.Health;
using RedditScrapper.Services.Routines;


IConfiguration config = new ConfigurationBuilder()
    .AddEnvironmentVariables(prefix: "REDDITSCRAPPER_")
    .Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();

builder.Services.AddScoped<IRoutineManagementService, RoutineManagementService>();
builder.Services.AddScoped<DatabaseHealthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RedditScrapperContext>(options => options.UseSqlServer(config.GetValue<string>("CONNECTIONSTRING")));

builder.Services.AddAutoMapper(typeof(RoutineProfile));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
