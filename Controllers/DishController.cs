using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DishController : ControllerBase
{
    private readonly DishService _dishService;
    private readonly ILogger<Assign1QueryController> _logger;

    public DishController(DishService dishService, ILogger<Assign1QueryController> logger)
    {
        _dishService = dishService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Dish>> Get()
    {
        _logger.LogInformation("Dish called Get (GET)");
        var dishs = _dishService.GetAllDishs();
        return Ok(dishs);
    }

    [HttpGet("{id}")]
    public ActionResult<Dish> Get(int id)
    {
        var dish = _dishService.GetDishById(id);
        if (dish == null)
        {
            return NotFound();
        }
        return Ok(dish);
    }


    [HttpPost]
    public IActionResult AddDish(Dish dish)
    {
        _logger.LogInformation("Dish called AddDish (POST) with Dish:{@Dish} ", dish);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDish = _dishService.AddDish(dish);
        return CreatedAtAction(nameof(Get), new { id = createdDish.DishId }, createdDish);
    }


    [HttpPut("{id}")]
    public IActionResult Put(int id, Dish dish)
    {
        _logger.LogInformation("Dish called Put (PUT) with ID:{Id} and Dish:{@Dish} ", id, dish);
        if (!_dishService.UpdateDish(id, dish))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Dish called Delete (DELETE) with ID: {Id}", id);
        if (!_dishService.DeleteDish(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
