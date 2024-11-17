using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CyclistController : ControllerBase
{
    private readonly CyclistService _cyclistService;

    public CyclistController(CyclistService cyclistService)
    {
        _cyclistService = cyclistService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cyclist>> Get()
    {
        var cyclists = _cyclistService.GetAllCyclists();
        return Ok(cyclists);
    }

    [HttpGet("{id}")]
    public ActionResult<Cyclist> Get(int id)
    {
        var cyclist = _cyclistService.GetCyclistById(id);
        if (cyclist == null)
        {
            return NotFound();
        }
        return Ok(cyclist);
    }

    [HttpPost]
    public ActionResult<Cyclist> Post(Cyclist cyclist)
    {
        var createdCyclist = _cyclistService.AddCyclist(cyclist);
        return CreatedAtAction(nameof(Get), new { id = createdCyclist.CyclistId }, createdCyclist);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cyclist cyclist)
    {
        if (!_cyclistService.UpdateCyclist(id, cyclist))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_cyclistService.DeleteCyclist(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
