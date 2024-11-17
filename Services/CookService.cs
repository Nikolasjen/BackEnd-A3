using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class CookService
{
    private readonly FoodAppG4Context _context;

    public CookService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Cook> GetAllCooks()
    {
        return _context.Cooks.ToList();
    }

    public Cook? GetCookById(int id)
    {
        return _context.Cooks.Find(id);
    }

    public Cook AddCook(Cook cook)
    {
        _context.Cooks.Add(cook);
        _context.SaveChanges();
        return cook;
    }

    public bool UpdateCook(int id, Cook cook)
    {
        if (id != cook.CookId)
        {
            return false;
        }

        _context.Entry(cook).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Cooks.Any(e => e.CookId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteCook(int id)
    {
        var cook = _context.Cooks.Find(id);
        if (cook == null)
        {
            return false;
        }

        _context.Cooks.Remove(cook);
        _context.SaveChanges();
        return true;
    }
}
