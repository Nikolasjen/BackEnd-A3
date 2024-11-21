using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CyclistController : ControllerBase
{
    private readonly CyclistService _cyclistService;
    private readonly ILogger<CyclistController> _logger;

    public CyclistController(CyclistService cyclistService, ILogger<CyclistController> logger)
    {
        _cyclistService = cyclistService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cyclist>> GetCyclist()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var cyclists = _cyclistService.GetAllCyclists();
        return Ok(cyclists);
    }

    [HttpGet("{id}")]
    public ActionResult<Cyclist> GetCyclistById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var cyclist = _cyclistService.GetCyclistById(id);
        if (cyclist == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(cyclist);
    }

    [HttpPost]
    public ActionResult<Cyclist> Post(Cyclist cyclist)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Cyclist: {@Cyclist}", "POST", userName, cyclist);
        var createdCyclist = _cyclistService.AddCyclist(cyclist);
        return CreatedAtAction(nameof(GetCyclist), new { id = createdCyclist.CyclistId }, createdCyclist);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cyclist cyclist)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Cyclist: {@Cyclist}", "PUT", id, userName, cyclist);
        if (!_cyclistService.UpdateCyclist(id, cyclist))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCyclistById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_cyclistService.DeleteCyclist(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
