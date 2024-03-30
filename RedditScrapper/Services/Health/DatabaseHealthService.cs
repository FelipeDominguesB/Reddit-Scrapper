using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RedditScrapper.Context;
using RedditScrapper.Model.DTOs.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Health
{
    public class DatabaseHealthService
    {
        private readonly RedditScrapperContext _dbContext;
        public DatabaseHealthService(RedditScrapperContext dbContext) 
        { 
            _dbContext = dbContext;
        }


        public PendingMigrationsDTO CheckPendingMigrations()
        {

            var pendingMigrations = _dbContext.Database.GetPendingMigrations();


            return new PendingMigrationsDTO
            {
                PendingMigrations = pendingMigrations.ToList(),
                PendingMigrationsCount = pendingMigrations.Count()
            };
        }
    }
}
