using Microsoft.EntityFrameworkCore;
using RedditScrapper.Context;
using RedditScrapper.Mapper;
using RedditScrapper.Services.Health;
using RedditScrapper.Services.Routines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRoutineService, RoutineService>();
builder.Services.AddScoped<DatabaseHealthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RedditScrapperContext>(options => options.UseSqlServer("Server=localhost;Database=RedditScrapper;Trusted_Connection=False;Encrypt=false; User Id=sa;Password=Pass@word1"));

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
