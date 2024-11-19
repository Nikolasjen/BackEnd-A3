using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CookController : ControllerBase
{
    private readonly CookService _cookService;
    private readonly ILogger<Assign1QueryController> _logger;

    public CookController(CookService cookService, ILogger<Assign1QueryController> logger)
    {
        _cookService = cookService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cook>> Get()
    {
        var cooks = _cookService.GetAllCooks();
        return Ok(cooks);
    }

    [HttpGet("{id}")]
    public ActionResult<Cook> Get(int id)
    {
        var cook = _cookService.GetCookById(id);
        if (cook == null)
        {
            return NotFound();
        }
        return Ok(cook);
    }

    [HttpPost]
    public ActionResult<Cook> Post(Cook cook)
    {
        _logger.LogInformation("Cook called Post (POST) with Cook:{@Cook} ", cook);

        var createdCook = _cookService.AddCook(cook);
        return CreatedAtAction(nameof(Get), new { id = createdCook.CookId }, createdCook);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cook cook)
    {
        _logger.LogInformation("Cook called Put (PUT) with ID:{Id} and Cook:{@Cook} ", id, cook);
        if (!_cookService.UpdateCook(id, cook))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Cook called Delete (DELETE) with ID: {Id}", id);

        if (!_cookService.DeleteCook(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
