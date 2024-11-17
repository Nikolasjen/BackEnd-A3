using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly TripService _tripService;

    public TripController(TripService tripService)
    {
        _tripService = tripService;
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
        var createdTrip = _tripService.AddTrip(trip);
        return CreatedAtAction(nameof(Get), new { id = createdTrip.TripId }, createdTrip);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Trip trip)
    {
        if (!_tripService.UpdateTrip(id, trip))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_tripService.DeleteTrip(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
