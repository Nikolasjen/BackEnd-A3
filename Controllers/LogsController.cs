using FoodAppG4.LoggingLevels;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodAppG4.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogService _logService;
        private readonly ILogger<LogsController> _logger;

        public LogsController(LogService logService, ILogger<LogsController> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<LogEntry>>> Get()
        {
            _logger.LogInformation("Operation: GetAllLogs");
            var logs = await _logService.GetAsync();
            return Ok(logs);
        }

        [HttpGet("{operation}")]
        public async Task<ActionResult<List<LogEntry>>> GetByOperation(string operation)
        {
            _logger.LogInformation("Operation: GetLogsByOperation, Operation: {Operation}", operation);
            var logs = await _logService.GetAsync(operation);
            return Ok(logs);
        }
    }
}
