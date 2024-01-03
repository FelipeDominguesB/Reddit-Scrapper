﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Context;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model.DTOs;
using RedditScrapper.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace RedditScrapper.Services.Routines
{
    public class RoutineService : IRoutineService
    {
        private readonly IServiceProvider _provider;
        public RoutineService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<RoutineHistory> AddHistoryToRoutine(long routineId, bool isSuccessful)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            Routine Routine = await dbContext.Routines.FirstAsync(x => x.Id == routineId);

            RoutineHistory RoutineHistory = new()
            {
                RoutineId = routineId,
                Succeded = isSuccessful,
                IsActive = true,
                CreationDate = DateTime.Now,
                Routine = Routine
            };

            RateEnum RoutineRate = (RateEnum)Routine.SyncRate;

            if (isSuccessful && RoutineRate != RateEnum.Once)
                Routine.NextRun = GetNextRunBasedOffRateEnum(RoutineRate);

            if (RoutineRate == RateEnum.Once)
                Routine.IsActive = false;

            await dbContext.SaveChangesAsync();

            return RoutineHistory;
        }

        public Task DisableRoutine(long routineId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Routine>> GetRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            return await dbContext.Routines.Where(routine => routine.IsActive).ToListAsync();
        }

        public async Task<ICollection<Routine>> GetPendingRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            return await dbContext.Routines.Where(routine => routine.IsActive && routine.NextRun <= DateTime.Now).ToListAsync();
        }

        public Task<ICollection<RoutineHistory>> GetRoutineHistory(long routineId)
        {
            throw new NotImplementedException();
        }

        public Task<Routine> RegisterRoutine(AddRoutineDTO addRoutineDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Routine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            throw new NotImplementedException();
        }


        private DateTime GetNextRunBasedOffRateEnum(RateEnum rate)
        {
            DateTime nextRun = DateTime.Now;


            switch (rate)
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
