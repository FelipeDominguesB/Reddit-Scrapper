using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RedditScrapper.Services.Health;

namespace RedditScrapper.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        private readonly DatabaseHealthService _healthCheckService;
        public HealthController(DatabaseHealthService healthCheckService) { 
            _healthCheckService = healthCheckService;
        }


        [HttpGet]
        [ActionName("check-database-health")]
        public async Task<ActionResult<bool>> CheckDatabaseHealth()
        {
            _healthCheckService.CheckPendingMigrations();


            return Ok(true);

        }
    }
}
