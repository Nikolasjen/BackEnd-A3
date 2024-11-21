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
    public ActionResult<IEnumerable<Rating>> GetAllRatings()
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}", "GET", userName);
        var ratings = _ratingService.GetAllRatings();
        return Ok(ratings);
    }

    [HttpGet("{id}")]
    public ActionResult<Rating> GetRatingById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "GET", id, userName);
        var rating = _ratingService.GetRatingById(id);
        if (rating == null)
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "GET", id, userName);
            return NotFound();
        }
        return Ok(rating);
    }

    [HttpPost]
    public ActionResult<Rating> CreateRating(Rating rating)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, User: {User}, Rating: {@Rating}", "POST", userName, rating);
        var createdRating = _ratingService.AddRating(rating);
        return CreatedAtAction(nameof(GetAllRatings), new { id = createdRating.RatingId }, createdRating);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRatingById(int id, Rating rating)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}, Rating: {@Rating}", "PUT", id, userName, rating);
        if (!_ratingService.UpdateRating(id, rating))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "PUT", id, userName);
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRatingById(int id)
    {
        var userName = (User.Identity?.Name ?? "Unknown").ToLower();
        _logger.LogInformation("Operation: {Operation}, Id: {Id}, User: {User}", "DELETE", id, userName);
        if (!_ratingService.DeleteRating(id))
        {
            _logger.LogWarning("Operation: {Operation}, Id: {Id} not found, User: {User}", "DELETE", id, userName);
            return NotFound();
        }

        return NoContent();
    }
}
