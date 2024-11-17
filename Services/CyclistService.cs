using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class CyclistService
{
    private readonly FoodAppG4Context _context;

    public CyclistService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Cyclist> GetAllCyclists()
    {
        return _context.Cyclists.ToList();
    }

    public Cyclist? GetCyclistById(int id)
    {
        return _context.Cyclists.Find(id);
    }

    public Cyclist AddCyclist(Cyclist cyclist)
    {
        _context.Cyclists.Add(cyclist);
        _context.SaveChanges();
        return cyclist;
    }

    public bool UpdateCyclist(int id, Cyclist cyclist)
    {
        if (id != cyclist.CyclistId)
        {
            return false;
        }

        _context.Entry(cyclist).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Cyclists.Any(e => e.CyclistId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteCyclist(int id)
    {
        var cyclist = _context.Cyclists.Find(id);
        if (cyclist == null)
        {
            return false;
        }

        _context.Cyclists.Remove(cyclist);
        _context.SaveChanges();
        return true;
    }
}
