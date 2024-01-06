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
        public RoutineExecutionService() 
        {
            
        }
        public Task<RoutineExecutionDTO> GetRoutineExecution(long routineExecutionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<RoutineExecutionSummaryDTO>> GetRoutineExecutionsSummary(long routineExecutionId)
        {
            throw new NotImplementedException();
        }

        public async Task<RoutineExecutionDTO> AddRoutineExecution(RoutineExecutionDTO routineExecutionDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<RoutineExecutionFileDTO> AddRoutineExecutionFile(RoutineExecutionFileDTO routineExecutionFileDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<RoutineExecutionFileDTO>> AddRoutineExecutionFiles(ICollection<RoutineExecutionFileDTO> routineExecutionFileDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<RoutineExecutionReportDTO>> GetRoutineExecutionReport(long routineExecutionId)
        {
            throw new NotImplementedException();
        }
    }
}
