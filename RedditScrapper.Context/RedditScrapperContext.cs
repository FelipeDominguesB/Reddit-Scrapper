using Microsoft.EntityFrameworkCore;
using RedditScrapper.Domain.Entities;

namespace RedditScrapper.Context
{
    public class RedditScrapperContext : DbContext
    {

        public DbSet<RoutineExecution> RoutinesExecutions { get; set; }
        public DbSet<RoutineExecutionFile> RoutineExecutionsFiles { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<User> Users { get; set; }
        public RedditScrapperContext(DbContextOptions<RedditScrapperContext> options) : base(options)
        {

        }
        
    }
}