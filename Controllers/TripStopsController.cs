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

    public TripStopController(TripStopService tripStopService)
    {
        _tripStopService = tripStopService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TripStop>> Get()
    {
        var tripStops = _tripStopService.GetAllTripStops();
        return Ok(tripStops);
    }

    [HttpGet("{id}")]
    public ActionResult<TripStop> Get(int id)
    {
        var tripStop = _tripStopService.GetTripStopById(id);
        if (tripStop == null)
        {
            return NotFound();
        }
        return Ok(tripStop);
    }

    [HttpPost]
    public ActionResult<TripStop> Post(TripStop tripStop)
    {
        var createdTripStop = _tripStopService.AddTripStop(tripStop);
        return CreatedAtAction(nameof(Get), new { id = createdTripStop.TripStopsId }, createdTripStop);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, TripStop tripStop)
    {
        if (!_tripStopService.UpdateTripStop(id, tripStop))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_tripStopService.DeleteTripStop(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
