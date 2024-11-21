using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TripStopController : ControllerBase
{
    private readonly TripStopService _tripStopService;
    private readonly ILogger<TripStopController> _logger;

    public TripStopController(TripStopService tripStopService, ILogger<TripStopController> logger)
    {
        _tripStopService = tripStopService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TripStop>> GetAllTripStops()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var tripStops = _tripStopService.GetAllTripStops();
        return Ok(tripStops);
    }

    [HttpGet("{id}")]
    public ActionResult<TripStop> GetTripStopById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var tripStop = _tripStopService.GetTripStopById(id);
        if (tripStop == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(tripStop);
    }

    [HttpPost]
    public ActionResult<TripStop> CreateTripStop(TripStop tripStop)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, TripStop: {@TripStop}", "POST", userName, tripStop);
        var createdTripStop = _tripStopService.AddTripStop(tripStop);
        return CreatedAtAction(nameof(GetAllTripStops), new { id = createdTripStop.TripStopsId }, createdTripStop);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTripStopById(int id, TripStop tripStop)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, TripStop: {@TripStop}", "PUT", id, userName, tripStop);
        if (!_tripStopService.UpdateTripStop(id, tripStop))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTripStopById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_tripStopService.DeleteTripStop(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
