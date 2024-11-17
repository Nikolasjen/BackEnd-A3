using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodAppG4.Services;

public class RatingService
{
    private readonly FoodAppG4Context _context;

    public RatingService(FoodAppG4Context context)
    {
        _context = context;
    }

    public IEnumerable<Rating> GetAllRatings()
    {
        return _context.Ratings.ToList();
    }

    public Rating? GetRatingById(int id)
    {
        return _context.Ratings.Find(id);
    }

    public Rating AddRating(Rating rating)
    {
        _context.Ratings.Add(rating);
        _context.SaveChanges();
        return rating;
    }

    public bool UpdateRating(int id, Rating rating)
    {
        if (id != rating.RatingId)
        {
            return false;
        }

        _context.Entry(rating).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Ratings.Any(e => e.RatingId == id))
            {
                return false;
            }
            throw;
        }
    }

    public bool DeleteRating(int id)
    {
        var rating = _context.Ratings.Find(id);
        if (rating == null)
        {
            return false;
        }

        _context.Ratings.Remove(rating);
        _context.SaveChanges();
        return true;
    }
}
