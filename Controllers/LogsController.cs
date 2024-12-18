using FoodAppG4.LoggingLevels;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers
{
    [Authorize(Policy = "AdminOnly")] // Ensure only Admins can access logs
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

        [HttpGet("advanced-search")]
        public async Task<ActionResult<List<LogEntry>>> AdvancedSearch(
            [FromQuery] string? user,
            [FromQuery] string? operation,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                return BadRequest("startDate cannot be later than endDate.");
            }

            var requester = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: AdvancedSearch, User: {Requester}, Filters - User: {User}, Operation: {Operation}, StartDate: {StartDate}, EndDate: {EndDate}",
                requester, user, operation, startDate, endDate);

            var logs = await _logService.SearchLogsAsync(user, operation, startDate, endDate);
            return Ok(logs);
        }
    }
}
