using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(OrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAllOrders()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrderById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder(Order order)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Order: {@Order}", "POST", userName, order); 
        var createdOrder = _orderService.AddOrder(order);
        return CreatedAtAction(nameof(GetAllOrders), new { id = createdOrder.OrderId }, createdOrder);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, Order order)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Order: {@Order}", "PUT", id, userName, order);
        if (!_orderService.UpdateOrder(id, order))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrderById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_orderService.DeleteOrder(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
