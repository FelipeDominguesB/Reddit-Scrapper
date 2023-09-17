using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Interface
{
    public interface IRoutineService
    {
        public Task<List<SyncRoutine>> GetPendingRoutines();
        public Task<SyncRoutine> RegisterRoutine(AddRoutineDTO addRoutineDTO);
        public Task<SyncRoutine> UpdateRoutine(SyncRoutine syncRoutine);
        public Task<SyncRoutine> GetRoutineHistory();
        public Task<SyncHistory> AddHistoryToRoutine(long routineId, bool successful);
        public Task<bool> DisableRoutine(long routineId);
    }
}
