using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly OrderDetailService _orderDetailService;

    public OrderDetailController(OrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
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
        var createdOrderDetail = _orderDetailService.AddOrderDetail(orderDetail);
        return CreatedAtAction(nameof(Get), new { id = createdOrderDetail.OrderDetailId }, createdOrderDetail);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, OrderDetail orderDetail)
    {
        if (!_orderDetailService.UpdateOrderDetail(id, orderDetail))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_orderDetailService.DeleteOrderDetail(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
