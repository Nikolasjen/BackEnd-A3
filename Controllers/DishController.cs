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
    private readonly ILogger<DishController> _logger;

    public DishController(DishService dishService, ILogger<DishController> logger)
    {
        _dishService = dishService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Dish>> GetAllDishes()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var dishs = _dishService.GetAllDishes();
        return Ok(dishs);
    }

    [HttpGet("{id}")]
    public ActionResult<Dish> GetDishById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var dish = _dishService.GetDishById(id);
        if (dish == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(dish);
    }


    [HttpPost]
    public IActionResult AddDish(Dish dish)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Dish: {@Dish}", "POST", userName, dish);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDish = _dishService.AddDish(dish);
        return CreatedAtAction(nameof(GetAllDishes), new { id = createdDish.DishId }, createdDish);
    }


    [HttpPut("{id}")]
    public IActionResult Put(int id, Dish dish)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Dish: {@Dish}", "PUT", id, userName, dish);
        if (!_dishService.UpdateDish(id, dish))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_dishService.DeleteDish(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
