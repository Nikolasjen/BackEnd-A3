using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class OrderService
{
    private readonly FoodAppG4Context _context;

    public OrderService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }

    public Order? GetOrderById(int id)
    {
        return _context.Orders.Find(id);
    }

    public Order AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }

    public bool UpdateOrder(int id, Order order)
    {
        if (id != order.OrderId)
        {
            return false;
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Orders.Any(e => e.OrderId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();
        return true;
    }
}
