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
    public ActionResult<IEnumerable<OrderDetail>> GetAllOrderDetails()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var orderDetails = _orderDetailService.GetAllOrderDetails();
        return Ok(orderDetails);
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDetail> GetOrderDetailById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var orderDetail = _orderDetailService.GetOrderDetailById(id);
        if (orderDetail == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(orderDetail);
    }

    [HttpPost]
    public ActionResult<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, OrderDetail: {@OrderDetail}", "POST", userName, orderDetail);
        var createdOrderDetail = _orderDetailService.AddOrderDetail(orderDetail);
        return CreatedAtAction(nameof(GetAllOrderDetails), new { id = createdOrderDetail.OrderDetailId }, createdOrderDetail);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrderDetailById(int id, OrderDetail orderDetail)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, OrderDetail: {@OrderDetail}", "PUT", id, userName, orderDetail);
        if (!_orderDetailService.UpdateOrderDetail(id, orderDetail))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrderDetailsById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_orderDetailService.DeleteOrderDetail(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
