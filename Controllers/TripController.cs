using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly TripService _tripService;
    private readonly ILogger<TripController> _logger;

    public TripController(TripService tripService, ILogger<TripController> logger)
    {
        _tripService = tripService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Trip>> GetAllTrips()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var trips = _tripService.GetAllTrips();
        return Ok(trips);
    }

    [HttpGet("{id}")]
    public ActionResult<Trip> GetTripById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var trip = _tripService.GetTripById(id);
        if (trip == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(trip);
    }

    [HttpPost]
    public ActionResult<Trip> CreateTrip(Trip trip)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Trip: {@Trip}", "POST", userName, trip);
        var createdTrip = _tripService.AddTrip(trip);
        return CreatedAtAction(nameof(GetAllTrips), new { id = createdTrip.TripId }, createdTrip);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTripById(int id, Trip trip)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Trip: {@Trip}", "PUT", id, userName, trip);
        if (!_tripService.UpdateTrip(id, trip))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTripById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_tripService.DeleteTrip(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
