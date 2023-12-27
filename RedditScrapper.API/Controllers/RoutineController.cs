using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using RedditScrapper.Domain.Entities;
using RedditScrapper.Model;
using RedditScrapper.DTOs;
using RedditScrapper.Interface;

namespace RedditScrapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineController : ControllerBase
    {

        private readonly IRoutineService _routineService;

        public RoutineController(IRoutineService routineService) {
            _routineService = routineService;
        }
        public async Task<ICollection<SyncRoutine>> Get() { 
            return await _routineService.GetRoutines();
        }
        public async Task<ICollection<SyncHistory>> GetRoutineHistory(long routineId) { 
            return await _routineService.GetRoutineHistory(routineId);
        }

        public async Task<SyncRoutine> AddRoutine(AddRoutineDTO addRoutineDTO)
        {
            SyncRoutine routine = await _routineService.RegisterRoutine(addRoutineDTO);
            return routine;
        }

        public async Task<SyncRoutine> UpdateRoutine(UpdateRoutineDTO updateRoutineDTO)
        {
            SyncRoutine routine = await _routineService.UpdateRoutine(updateRoutineDTO);
            return routine;
        }

        public async Task DisableRoutine(long routineId)
        {
            await _routineService.DisableRoutine(routineId);
        }
    }
}
