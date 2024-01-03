using Microsoft.EntityFrameworkCore;
using RedditScrapper.Domain.Entities;

namespace RedditScrapper.Context
{
    public class RedditScrapperContext : DbContext
    {

        public DbSet<RoutineHistory> RoutinesHistory { get; set; }

        public DbSet<Routine> Routines { get; set; }
        public DbSet<User> Users { get; set; }
        public RedditScrapperContext(DbContextOptions<RedditScrapperContext> options) : base(options)
        {

        }
        
    }
}