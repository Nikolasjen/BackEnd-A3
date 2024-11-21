using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodAppG4.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CookController : ControllerBase
    {
        private readonly CookService _cookService;
        private readonly ILogger<CookController> _logger;

        public CookController(CookService cookService, ILogger<CookController> logger)
        {
            _cookService = cookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cook>> GetAllCooks()
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
            var cooks = _cookService.GetAllCooks();
            return Ok(cooks);
        }

        [HttpGet("{id}")]
        public ActionResult<Cook> GetCookById(int id)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
            var cook = _cookService.GetCookById(id);
            if (cook == null)
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
                return NotFound();
            }
            return Ok(cook);
        }

        [HttpPost]
        public ActionResult<Cook> CreateCook(Cook cook)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, User: {User}, Cook: {@Cook}", "POST", userName, cook);
            var createdCook = _cookService.AddCook(cook);
            return CreatedAtAction(nameof(GetAllCooks), new { id = createdCook.CookId }, createdCook);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCookById(int id, Cook cook)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Cook: {@Cook}", "PUT", id, userName, cook);
            if (!_cookService.UpdateCook(id, cook))
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCookById(int id)
        {
            var userName = (User.Identity?.Name ?? "Unknown").ToLower();
            _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
            if (!_cookService.DeleteCook(id))
            {
                _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
                return NotFound();
            }

            return NoContent();
        }
    }
}
