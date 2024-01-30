﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedditScrapper.Context;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Exceptions;
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
        private readonly IMapper _mapper;
        public RoutineService(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<RoutineExecutionDTO> AddRoutineExecution(RoutineExecutionDTO routineExecutionDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            Routine Routine = await dbContext.Routines.FirstAsync(x => x.Id == routineExecutionDTO.RoutineId);

            RoutineExecution routineExecution = _mapper.Map<RoutineExecutionDTO, RoutineExecution>(routineExecutionDTO);

            routineExecution.IsActive = true;
            routineExecution.CreationDate = DateTime.Now;
            
            RateEnum RoutineRate = (RateEnum) Routine.SyncRate;

            if (routineExecutionDTO.Succeded && RoutineRate != RateEnum.Once)
                Routine.NextRun = GetNextRunBasedOffRateEnum(RoutineRate);

            if (RoutineRate == RateEnum.Once)
                Routine.IsActive = false;

            await dbContext.SaveChangesAsync();

            return _mapper.Map<RoutineExecution, RoutineExecutionDTO>(routineExecution);
        }

        
        public async Task<ICollection<RoutineDTO>> GetRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            
            List<Routine> routines = await dbContext.Routines.Where(routine => routine.IsActive).ToListAsync();

            return _mapper.Map<List<RoutineDTO>>(routines);
        }

        public async Task<ICollection<Routine>> GetPendingRoutines()
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            return await dbContext.Routines.Where(routine => routine.IsActive && routine.NextRun <= DateTime.Now).ToListAsync();
        }

        public async Task<ICollection<RoutineExecutionDTO>> GetRoutineExecution(long routineId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            List<RoutineExecution> routineExecutions = await dbContext.RoutinesExecutions.AsNoTracking().Where(x => x.RoutineId == routineId).ToListAsync();

            return _mapper.Map<List<RoutineExecutionDTO>>(routineExecutions);
        }

        public async Task<RoutineDTO> RegisterRoutine(AddRoutineDTO addRoutineDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            Routine routine = new Routine()
            {
                CreationDate = DateTime.Now,
                IsActive = true,
                UserId = 1,
                PostSorting = (int)addRoutineDTO.PostSorting,
                MaxPostsPerSync = addRoutineDTO.MaxPostsPerSync,
                SubredditName = addRoutineDTO.SubredditName,
                NextRun = addRoutineDTO.RunImmediatly ? DateTime.Now : this.GetNextRunBasedOffRateEnum(addRoutineDTO.SyncRate),
                SyncRate = (int)addRoutineDTO.SyncRate
            };

            dbContext.Routines.Add(routine);
            
            await dbContext.SaveChangesAsync();

            return _mapper.Map<RoutineDTO>(routine);
        }

        public Task<RoutineDTO> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            
            throw new NotImplementedException();
        }

        public async Task EnableRoutine(long routineId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            Routine? routine = await dbContext.Routines.Where(routine => routine.Id == routineId).FirstOrDefaultAsync();

            if (routine == null)
                throw new EntityNotFoundException();

            routine.IsActive = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task DisableRoutine(long routineId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();
            Routine? routine = await dbContext.Routines.Where(routine => routine.Id == routineId).FirstOrDefaultAsync();

            if (routine == null)
                throw new EntityNotFoundException();

            routine.IsActive = false;

            await dbContext.SaveChangesAsync();
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

        public Task<ICollection<RoutineExecutionReportDTO>> GetRoutineExecutionReport(long routineExecutionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<RoutineExecutionSummaryDTO>> GetRoutineExecutionsSummary(long routineExecutionId)
        {
            throw new NotImplementedException();
        }

        public Task<RoutineExecutionFileDTO> AddRoutineExecutionFile(RoutineExecutionFileDTO routineExecutionFileDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<RoutineExecutionFileDTO>> AddRoutineExecutionFiles(ICollection<RoutineExecutionFileDTO> routineExecutionFileDTO)
        {
            throw new NotImplementedException();
        }
    }
}
