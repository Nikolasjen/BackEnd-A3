using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly RatingService _ratingService;

    public RatingController(RatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Rating>> Get()
    {
        var ratings = _ratingService.GetAllRatings();
        return Ok(ratings);
    }

    [HttpGet("{id}")]
    public ActionResult<Rating> Get(int id)
    {
        var rating = _ratingService.GetRatingById(id);
        if (rating == null)
        {
            return NotFound();
        }
        return Ok(rating);
    }

    [HttpPost]
    public ActionResult<Rating> Post(Rating rating)
    {
        var createdRating = _ratingService.AddRating(rating);
        return CreatedAtAction(nameof(Get), new { id = createdRating.RatingId }, createdRating);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Rating rating)
    {
        if (!_ratingService.UpdateRating(id, rating))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_ratingService.DeleteRating(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
