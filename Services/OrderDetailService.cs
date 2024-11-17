using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class OrderDetailService
{
    private readonly FoodAppG4Context _context;

    public OrderDetailService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<OrderDetail> GetAllOrderDetails()
    {
        return _context.OrderDetails.ToList();
    }

    public OrderDetail? GetOrderDetailById(int id)
    {
        return _context.OrderDetails.Find(id);
    }

    public OrderDetail AddOrderDetail(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        _context.SaveChanges();
        return orderDetail;
    }

    public bool UpdateOrderDetail(int id, OrderDetail orderDetail)
    {
        if (id != orderDetail.OrderDetailId)
        {
            return false;
        }

        _context.Entry(orderDetail).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.OrderDetails.Any(e => e.OrderDetailId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteOrderDetail(int id)
    {
        var orderDetail = _context.OrderDetails.Find(id);
        if (orderDetail == null)
        {
            return false;
        }

        _context.OrderDetails.Remove(orderDetail);
        _context.SaveChanges();
        return true;
    }
}
