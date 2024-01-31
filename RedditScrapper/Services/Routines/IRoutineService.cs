using RedditScrapper.Domain.Entities;
using RedditScrapper.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Routines
{
    public interface IRoutineService
    {
        public Task<ICollection<RoutineDTO>> GetRoutines();
        public Task<ICollection<Routine>> GetPendingRoutines();
        public Task<RoutineDTO> RegisterRoutine(AddRoutineDTO addRoutineDTO);
        public Task<RoutineDTO> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO);
        public Task<ICollection<RoutineExecutionDTO>> GetRoutineExecution(long routineId);
        public Task<RoutineExecutionDTO> AddRoutineExecution(RoutineExecutionDTO routineExecutionDTO);
        public Task<RoutineExecutionDTO> UpdateRoutineExecution(RoutineExecutionDTO routineExecutionDTO);
        public Task EnableRoutine(long routineId);
        public Task DisableRoutine(long routineId);

        public Task<ICollection<RoutineExecutionReportDTO>> GetRoutineExecutionReport(long routineExecutionId);
        public Task<ICollection<RoutineExecutionSummaryDTO>> GetRoutineExecutionsSummary(long routineExecutionId);
        public Task<RoutineExecutionFileDTO> AddRoutineExecutionFile(RoutineExecutionFileDTO routineExecutionFileDTO);
        public Task<ICollection<RoutineExecutionFileDTO>> AddRoutineExecutionFiles(ICollection<RoutineExecutionFileDTO> routineExecutionFileDTO);
    }
}
