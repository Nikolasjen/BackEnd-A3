using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class TripStopService
{
    private readonly FoodAppG4Context _context;

    public TripStopService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<TripStop> GetAllTripStops()
    {
        return _context.TripStops.ToList();
    }

    public TripStop? GetTripStopById(int id)
    {
        return _context.TripStops.Find(id);
    }

    public TripStop AddTripStop(TripStop tripStop)
    {
        _context.TripStops.Add(tripStop);
        _context.SaveChanges();
        return tripStop;
    }

    public bool UpdateTripStop(int id, TripStop tripStop)
    {
        if (id != tripStop.TripStopsId)
        {
            return false;
        }

        _context.Entry(tripStop).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TripStops.Any(e => e.TripStopsId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteTripStop(int id)
    {
        var tripStop = _context.TripStops.Find(id);
        if (tripStop == null)
        {
            return false;
        }

        _context.TripStops.Remove(tripStop);
        _context.SaveChanges();
        return true;
    }
}
