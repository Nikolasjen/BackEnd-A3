using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class DishService
{
    private readonly FoodAppG4Context _context;

    public DishService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Dish> GetAllDishes()
    {
        return _context.Dishes.ToList();
    }

    public Dish? GetDishById(int id)
    {
        return _context.Dishes.Find(id);
    }

    public Dish AddDish(Dish dish)
    {
        _context.Dishes.Add(dish);
        _context.SaveChanges();
        return dish;
    }

    public bool UpdateDish(int id, Dish dish)
    {
        if (id != dish.DishId)
        {
            return false;
        }

        _context.Entry(dish).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Dishes.Any(e => e.DishId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteDish(int id)
    {
        var dish = _context.Dishes.Find(id);
        if (dish == null)
        {
            return false;
        }

        _context.Dishes.Remove(dish);
        _context.SaveChanges();
        return true;
    }
}
