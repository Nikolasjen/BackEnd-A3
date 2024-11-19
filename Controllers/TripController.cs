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
    public ActionResult<IEnumerable<Trip>> Get()
    {
        var trips = _tripService.GetAllTrips();
        return Ok(trips);
    }

    [HttpGet("{id}")]
    public ActionResult<Trip> Get(int id)
    {
        var trip = _tripService.GetTripById(id);
        if (trip == null)
        {
            return NotFound();
        }
        return Ok(trip);
    }

    [HttpPost]
    public ActionResult<Trip> Post(Trip trip)
    {
        _logger.LogInformation("Trip called Post (POST) with Trip:{@Trip} ", trip);
        var createdTrip = _tripService.AddTrip(trip);
        return CreatedAtAction(nameof(Get), new { id = createdTrip.TripId }, createdTrip);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Trip trip)
    {
        _logger.LogInformation("Trip called Put (PUT) with ID:{Id} and Trip:{@Trip} ", id, trip);
        if (!_tripService.UpdateTrip(id, trip))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Trip called Delete (DELETE) with ID:{Id} ", id);
        if (!_tripService.DeleteTrip(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
