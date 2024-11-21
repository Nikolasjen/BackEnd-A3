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
    private readonly ILogger<CookController> _logger;

    public CookController(CookService cookService, ILogger<CookController> logger)
    {
        _cookService = cookService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cook>> GetAllCooks()
    {
        _logger.LogInformation("Operation: GetAllCooks");

        var cooks = _cookService.GetAllCooks();
        return Ok(cooks);
    }

    [HttpGet("{id}")]
    public ActionResult<Cook> GetCookById(int id)
    {
        _logger.LogInformation("Operation: GetCookById, Id: {Id}", id);

        var cook = _cookService.GetCookById(id);
        if (cook == null)
        {
            return NotFound();
        }
        return Ok(cook);
    }

    [HttpPost]
    public ActionResult<Cook> AddCook(Cook cook)
    {
        _logger.LogInformation("Operation: CreateCook, Cook: {@Cook}", cook);

        var createdCook = _cookService.AddCook(cook);
        return CreatedAtAction(nameof(AddCook), new { id = createdCook.CookId }, createdCook);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCook(int id, Cook cook)
    {
        _logger.LogInformation("Operation: UpdateCook, Id: {Id}, Cook: {@Cook}", id, cook);
        if (!_cookService.UpdateCook(id, cook))
        {
            _logger.LogWarning("Operation: UpdateCook, Id: {Id} not found.", id);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCook(int id)
    {
        _logger.LogInformation("Operation: DeleteCook, Id: {Id}", id);

        if (!_cookService.DeleteCook(id))
        {
            _logger.LogWarning("Operation: DeleteCook, Id: {Id} not found.", id);
            return NotFound();
        }

        return NoContent();
    }
}
