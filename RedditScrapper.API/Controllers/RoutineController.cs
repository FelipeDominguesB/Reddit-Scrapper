using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using RedditScrapper.Services.Routines;
using RedditScrapper.Model.DTOs.Routine;

namespace RedditScrapper.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {

        private readonly IRoutineService _routineService;

        public RoutineController(IRoutineService routineService, IConfiguration configuration) {
            _routineService = routineService;
            configuration.GetChildren();
        }

        [HttpGet]
        [ActionName("routines")]
        public async Task<ICollection<RoutineDTO>> Get() { 
            return await _routineService.GetRoutines();
        }

        [HttpGet]
        [ActionName("routine-executions")]
        public async Task<ICollection<RoutineExecutionDTO>> GetRoutineExecutions(long routineId)
        {
            return await _routineService.GetRoutineExecution(routineId);
        }

        [HttpPost]
        [ActionName("add-routine")]
        public async Task<RoutineDTO> AddRoutine(AddRoutineDTO addRoutineDTO)
        {
            RoutineDTO routine = await _routineService.RegisterRoutine(addRoutineDTO);
            return routine;
        }

        [HttpPut]
        [ActionName("update-routine")]
        public async Task<RoutineDTO> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            RoutineDTO routine = await _routineService.UpdateRoutine(updateRoutineDTO);
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
