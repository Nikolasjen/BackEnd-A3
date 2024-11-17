using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class TripService
{
    private readonly FoodAppG4Context _context;

    public TripService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Trip> GetAllTrips()
    {
        return _context.Trips.ToList();
    }

    public Trip? GetTripById(int id)
    {
        return _context.Trips.Find(id);
    }

    public Trip AddTrip(Trip trip)
    {
        _context.Trips.Add(trip);
        _context.SaveChanges();
        return trip;
    }

    public bool UpdateTrip(int id, Trip trip)
    {
        if (id != trip.TripId)
        {
            return false;
        }

        _context.Entry(trip).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Trips.Any(e => e.TripId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteTrip(int id)
    {
        var trip = _context.Trips.Find(id);
        if (trip == null)
        {
            return false;
        }

        _context.Trips.Remove(trip);
        _context.SaveChanges();
        return true;
    }
}
