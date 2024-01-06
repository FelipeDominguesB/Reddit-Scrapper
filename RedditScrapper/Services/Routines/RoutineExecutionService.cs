using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RedditScrapper.Context;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Exceptions;
using RedditScrapper.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Routines
{
    public class RoutineExecutionService : IRoutineExecutionService
    {
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;
        public RoutineExecutionService(IServiceProvider provider, IMapper mapper) 
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<RoutineExecutionDTO> GetRoutineExecution(long routineExecutionId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            RoutineExecution? routineExecution = dbContext.RoutinesExecutions.FirstOrDefault(x => x.Id == routineExecutionId);

            if (routineExecution == null)
                throw new EntityNotFoundException();

            return _mapper.Map<RoutineExecutionDTO>(routineExecution);
        }

        //TODO: IMPLEMENTATION
        public Task<ICollection<RoutineExecutionSummaryDTO>> GetRoutineExecutionsSummary(long routineExecutionId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            throw new NotImplementedException();
        }

        public async Task<RoutineExecutionDTO> AddRoutineExecution(RoutineExecutionDTO routineExecutionDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            RoutineExecution routineExecution = _mapper.Map<RoutineExecution>(routineExecutionDTO);
            dbContext.RoutinesExecutions.Add(routineExecution);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<RoutineExecutionDTO>(routineExecution);
        }

        public async Task<RoutineExecutionFileDTO> AddRoutineExecutionFile(RoutineExecutionFileDTO routineExecutionFileDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            RoutineExecutionFile routineExecutionFile = _mapper.Map<RoutineExecutionFile>(routineExecutionFileDTO);
            dbContext.RoutineExecutionsFiles.Add(routineExecutionFile);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<RoutineExecutionFileDTO>(routineExecutionFile);
        }

        public async Task<ICollection<RoutineExecutionFileDTO>> AddRoutineExecutionFiles(ICollection<RoutineExecutionFileDTO> routineExecutionFilesDTO)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            List<RoutineExecutionFile> routineExecutionFiles = (List<RoutineExecutionFile>) _mapper.Map<ICollection<RoutineExecutionFile>>(routineExecutionFilesDTO);
            
            foreach(RoutineExecutionFile routineExecutionFile in routineExecutionFiles)
                dbContext.RoutineExecutionsFiles.Add(routineExecutionFile);

            await dbContext.SaveChangesAsync();

            return _mapper.Map<ICollection<RoutineExecutionFileDTO>>(routineExecutionFiles);
        }
        
        //TODO: IMPLEMENTATION
        public Task<ICollection<RoutineExecutionReportDTO>> GetRoutineExecutionReport(long routineExecutionId)
        {
            using IServiceScope serviceProviderScope = _provider.CreateScope();
            RedditScrapperContext dbContext = serviceProviderScope.ServiceProvider.GetRequiredService<RedditScrapperContext>();

            throw new NotImplementedException();
        }
    }
}
