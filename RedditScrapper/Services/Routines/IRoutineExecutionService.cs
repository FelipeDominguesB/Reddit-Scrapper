using RedditScrapper.Domain.Entities;
using RedditScrapper.Model.DTOs.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Services.Routines
{
    public interface IRoutineExecutionService
    {
        public Task RunPendingRoutines();
        public Task RunRoutine(Routine routine);

    }
}
