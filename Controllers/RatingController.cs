using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly RatingService _ratingService;
    private readonly ILogger<RatingController> _logger;

    public RatingController(RatingService ratingService, ILogger<RatingController> logger)
    {
        _ratingService = ratingService;
        _logger = logger;
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
        _logger.LogInformation("Rating called Post (POST) with Rating:{@Rating} ", rating);
        var createdRating = _ratingService.AddRating(rating);
        return CreatedAtAction(nameof(Get), new { id = createdRating.RatingId }, createdRating);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Rating rating)
    {
        _logger.LogInformation("Rating called Put (PUT) with ID:{Id} and Rating:{@Rating} ", id, rating);
        if (!_ratingService.UpdateRating(id, rating))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Rating called Delete (DELETE) with ID:{Id} ", id);
        if (!_ratingService.DeleteRating(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
