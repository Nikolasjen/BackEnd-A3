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

    public DishController(DishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Dish>> Get()
    {
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
        if (!_dishService.UpdateDish(id, dish))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_dishService.DeleteDish(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
