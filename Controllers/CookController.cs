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

    public CookController(CookService cookService)
    {
        _cookService = cookService;
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
        var createdCook = _cookService.AddCook(cook);
        return CreatedAtAction(nameof(Get), new { id = createdCook.CookId }, createdCook);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Cook cook)
    {
        if (!_cookService.UpdateCook(id, cook))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_cookService.DeleteCook(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
