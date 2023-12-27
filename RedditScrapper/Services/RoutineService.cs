using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Context;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Interface;
using RedditScrapper.DTOs;
using RedditScrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace RedditScrapper.Services
{
    public class RoutineService : IRoutineService
    {
        private readonly IServiceProvider _provider;
        public RoutineService(IServiceProvider provider) {
            _provider = provider;
        }

        public async Task<SyncHistory> AddHistoryToRoutine(long routineId, bool isSuccessful)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            
            SyncRoutine syncRoutine = await dbContext.SyncRoutines.FirstAsync(x => x.Id == routineId);

            SyncHistory syncHistory = new()
            {
                RoutineId = routineId,
                Succeded = isSuccessful,
                IsActive = true,
                CreationDate = DateTime.Now,
                SyncRoutine = syncRoutine
            };

            RateEnum syncRoutineRate = (RateEnum)syncRoutine.SyncRate;

            if (isSuccessful && syncRoutineRate != RateEnum.Once)
                syncRoutine.NextRun = GetNextRunBasedOffRateEnum(syncRoutineRate);

            if (syncRoutineRate == RateEnum.Once)
                syncRoutine.IsActive = false;

            await dbContext.SaveChangesAsync();

            return syncHistory;
        }

        public Task DisableRoutine(long routineId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SyncRoutine>> GetRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            return await dbContext.SyncRoutines.Where(routine => routine.IsActive).ToListAsync();
        }

        public async Task<ICollection<SyncRoutine>> GetPendingRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            return await dbContext.SyncRoutines.Where(routine => routine.IsActive && routine.NextRun <= DateTime.Now).ToListAsync();
        }

        public Task<ICollection<SyncHistory>> GetRoutineHistory(long routineId)
        {
            throw new NotImplementedException();
        }

        public Task<SyncRoutine> RegisterRoutine(AddRoutineDTO addRoutineDTO)
        {
            throw new NotImplementedException();
        }

        public Task<SyncRoutine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            throw new NotImplementedException();
        }


        private DateTime GetNextRunBasedOffRateEnum(RateEnum rate)
        {
            DateTime nextRun = DateTime.Now;

            
            switch(rate)
            {
                case RateEnum.Daily:
                    nextRun = nextRun.AddDays(1); break;
                case RateEnum.Weekly:
                    nextRun = nextRun.AddDays(7); break;
                case RateEnum.Monthly:
                    nextRun = nextRun.AddMonths(1); break;
                case RateEnum.Yearly:
                    nextRun = nextRun.AddYears(1); break;
                default:
                    break;
                
            }

            return nextRun;
        }
    }
}
