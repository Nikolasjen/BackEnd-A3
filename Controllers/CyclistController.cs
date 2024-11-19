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
    private readonly ILogger<Assign1QueryController> _logger;

    public CyclistController(CyclistService cyclistService, ILogger<Assign1QueryController> logger)
    {
        _cyclistService = cyclistService;
        _logger = logger;
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
        _logger.LogInformation("Cyclist called Post (POST) with Cyclist:{@Cyclist} ", cyclist);
        var createdCyclist = _cyclistService.AddCyclist(cyclist);
        return CreatedAtAction(nameof(Get), new { id = createdCyclist.CyclistId }, createdCyclist);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cyclist cyclist)
    {
        _logger.LogInformation("Cyclist called Put (PUT) with ID:{Id} and Cyclist:{@Cyclist} ", id, cyclist);
        if (!_cyclistService.UpdateCyclist(id, cyclist))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Cyclist called Delete (DELETE) with ID:{Id} ", id);
        if (!_cyclistService.DeleteCyclist(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
