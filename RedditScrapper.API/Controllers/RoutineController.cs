using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using RedditScrapper.Services.Routines;
using RedditScrapper.Model.DTOs;

namespace RedditScrapper.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {

        private readonly IRoutineService _routineService;

        public RoutineController(IRoutineService routineService) {
            _routineService = routineService;
        }

        [HttpGet]
        [ActionName("routines")]
        public async Task<ICollection<Routine>> Get() { 
            return await _routineService.GetRoutines();
        }

        [HttpGet]
        [ActionName("routine-history")]
        public async Task<ICollection<RoutineExecution>> GetRoutineHistory(long routineId)
        {
            return await _routineService.GetRoutineHistory(routineId);
        }

        [HttpPost]
        [ActionName("add-routine")]
        public async Task<Routine> AddRoutine(AddRoutineDTO addRoutineDTO)
        {
            Routine routine = await _routineService.RegisterRoutine(addRoutineDTO);
            return routine;
        }

        [HttpPut]
        [ActionName("update-routine")]
        public async Task<Routine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            Routine routine = await _routineService.UpdateRoutine(updateRoutineDTO);
            return routine;
        }

        [HttpPut]
        [ActionName("enable-routine")]
        public async Task EnableRoutine(long routineId)
        {
            await _routineService.EnableRoutine(routineId);
        }

        [HttpDelete]
        [ActionName("disable-routine")]
        public async Task DisableRoutine(long routineId)
        {
            await _routineService.DisableRoutine(routineId);
        }
    }
}
