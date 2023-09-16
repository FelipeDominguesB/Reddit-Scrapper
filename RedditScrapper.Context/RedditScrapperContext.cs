using Microsoft.EntityFrameworkCore;
using RedditScrapper.Domain.Entities;

namespace RedditScrapper.Context
{
    public class RedditScrapperContext : DbContext
    {

        public DbSet<SyncHistory> SyncHistory { get; set; }

        public DbSet<SyncRoutine> SyncRoutines { get; set; }
        public RedditScrapperContext(DbContextOptions<RedditScrapperContext> options) : base(options)
        {

        }
        
    }
}