using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly OrderDetailService _orderDetailService;
    private readonly ILogger<OrderDetailController> _logger;

    public OrderDetailController(OrderDetailService orderDetailService, ILogger<OrderDetailController> logger)
    {
        _orderDetailService = orderDetailService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrderDetail>> Get()
    {
        var orderDetails = _orderDetailService.GetAllOrderDetails();
        return Ok(orderDetails);
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDetail> Get(int id)
    {
        var orderDetail = _orderDetailService.GetOrderDetailById(id);
        if (orderDetail == null)
        {
            return NotFound();
        }
        return Ok(orderDetail);
    }

    [HttpPost]
    public ActionResult<OrderDetail> Post(OrderDetail orderDetail)
    {
        _logger.LogInformation("OrderDetail called Post (POST) with OrderDetail:{@OrderDetail} ", orderDetail);
        var createdOrderDetail = _orderDetailService.AddOrderDetail(orderDetail);
        return CreatedAtAction(nameof(Get), new { id = createdOrderDetail.OrderDetailId }, createdOrderDetail);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, OrderDetail orderDetail)
    {
        _logger.LogInformation("OrderDetail called Put (PUT) with ID:{Id} and OrderDetail:{@OrderDetail} ", id, orderDetail);
        if (!_orderDetailService.UpdateOrderDetail(id, orderDetail))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("OrderDetail called Delete (DELETE) with ID:{Id} ", id);
        if (!_orderDetailService.DeleteOrderDetail(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
