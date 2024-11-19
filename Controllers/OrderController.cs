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
    private readonly ILogger<Assign1QueryController> _logger;

    public OrderController(OrderService orderService, ILogger<Assign1QueryController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get()
    {
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> Get(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> Post(Order order)
    {
        _logger.LogInformation("Order called Post (POST) with Order:{@Order} ", order);
        var createdOrder = _orderService.AddOrder(order);
        return CreatedAtAction(nameof(Get), new { id = createdOrder.OrderId }, createdOrder);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Order order)
    {
        _logger.LogInformation("Order called Put (PUT) with ID:{Id} and Order:{@Order} ", id, order);
        if (!_orderService.UpdateOrder(id, order))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Order called Delete (DELETE) with ID:{Id} ", id);
        if (!_orderService.DeleteOrder(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
