using RedditScrapper.Domain.Entities;
using RedditScrapper.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IRoutineService
    {
        public Task<ICollection<SyncRoutine>> GetRoutines();
        public Task<ICollection<SyncRoutine>> GetPendingRoutines();
        public Task<SyncRoutine> RegisterRoutine(AddRoutineDTO addRoutineDTO);
        public Task<SyncRoutine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO);
        public Task<ICollection<SyncHistory>> GetRoutineHistory(long routineId);
        public Task<SyncHistory> AddHistoryToRoutine(long routineId, bool successful);
        public Task DisableRoutine(long routineId);
    }
}
