// using FoodAppG4.Data;
// using FoodAppG4.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace FoodAppG4.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class CookController : ControllerBase
// {
//     private readonly FoodAppG4Context _context;

//     // Constructor injection of the DbContext
//     public CookController(FoodAppG4Context context)
//     {
//         _context = context;
//     }

//     // HTTP GET to return a list of cooks
//     [HttpGet]
//     public ActionResult<IEnumerable<Cook>> Get()
//     {
//         var cooks = _context.Cooks.ToList();
//         return Ok(cooks); // Return 200 OK with the list of cooks
//     }
// }


using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

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
