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
        public Task<ICollection<Routine>> GetRoutines();
        public Task<ICollection<Routine>> GetPendingRoutines();
        public Task<Routine> RegisterRoutine(AddRoutineDTO addRoutineDTO);
        public Task<Routine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO);
        public Task<ICollection<RoutineHistory>> GetRoutineHistory(long routineId);
        public Task<RoutineHistory> AddHistoryToRoutine(long routineId, bool successful);
        public Task DisableRoutine(long routineId);
    }
}
