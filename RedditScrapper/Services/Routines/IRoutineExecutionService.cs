using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedditScrapper.Model.DTOs;

namespace RedditScrapper.Services.Routines
{
    public interface IRoutineExecutionService
    {

        public Task<RoutineExecutionDTO> GetRoutineExecution(long routineExecutionId);
        public Task<ICollection<RoutineExecutionReportDTO>> GetRoutineExecutionReport(long routineExecutionId);
        public Task<ICollection<RoutineExecutionSummaryDTO>> GetRoutineExecutionsSummary(long routineExecutionId);
        public Task<RoutineExecutionDTO> AddRoutineExecution(RoutineExecutionDTO routineExecutionDTO);
        public Task<RoutineExecutionFileDTO> AddRoutineExecutionFile(RoutineExecutionFileDTO routineExecutionFileDTO); 
        public Task<ICollection<RoutineExecutionFileDTO>> AddRoutineExecutionFiles(ICollection<RoutineExecutionFileDTO> routineExecutionFileDTO); 
    }
}
