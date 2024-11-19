using FoodAppG4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodAppG4.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Assign1QueryController : ControllerBase
    {
        private readonly QueryService _queryService;
        private readonly CookService _cookService;
        // private readonly ILogger<Assign1QueryController> _logger;

        // public Assign1QueryController(QueryService queryService, CookService cookService, ILogger<Assign1QueryController> logger)
        public Assign1QueryController(QueryService queryService, CookService cookService)
        {
            _queryService = queryService;
            _cookService = cookService;
            // _logger = logger;
        }

        // C.1: Get data for each cook
        [HttpGet("c1_cooks")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        [Authorize(Policy = "AdminOrManagerOnly")]
        public IActionResult GetCookData()
        {
            // _logger.LogInformation("GET called GetCookData");

            var cooks = _queryService.GetCookData();
            return Ok(cooks);
        }

        // C.2: Get available dish details for a specific cook's kitchen
        [HttpGet("c2_dishes/{cookId}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        [AllowAnonymous]
        public IActionResult GetDishDetailsForCook(int cookId)
        {
            // _logger.LogInformation("GET called GetDishDetailsForCook with ID:{} ", cookId);

            var dishes = _queryService.GetDishDetailsForCook(cookId);
            return Ok(dishes);
        }

        // C.3: Get list of dishes and kitchen for an order
        [HttpGet("c3_orders/{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var orderDetails = _queryService.GetOrderDetails(orderId);
            return Ok(orderDetails);
        }

        // C.4: Get the trip details for a cyclist
        [HttpGet("c4_trips/{tripId}")]
        public IActionResult GetTripDetails(int tripId)
        {
            var tripDetails = _queryService.GetTripDetails(tripId);
            return Ok(tripDetails);
        }

        // C.5: Get the average rating for a cook
        [HttpGet("c5_ratings/cook/{cookId}")]
        [Authorize(Policy = "AdminOrCookOnly")]
        public IActionResult GetCookAverageRating(int cookId)
        {

            // Retrieve the IsAdmin claim
            var isAdmin = User.HasClaim("IsAdmin", "true");

            if (!isAdmin)
            {
                // Retrieve the CookId claim as a string
                var userCookIdStr = User.FindFirstValue("CookId");

                if (string.IsNullOrEmpty(userCookIdStr) || !int.TryParse(userCookIdStr, out int userCookId))
                {
                    // CookId claim is missing or invalid
                    return Unauthorized("Cook information is missing or invalid.");
                }

                if (userCookId != cookId)
                {
                    // The cookId in the URL does not match the user's CookId
                    return Unauthorized("You are not authorized to view this data.");
                }
            }


            var rating = _queryService.GetCookAverageRating(cookId);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // C.5.1: Get average rating for all cyclists
        [HttpGet("c5_1_ratings/cyclists")]
        public IActionResult GetAverageRatingForCyclists()
        {
            var ratings = _queryService.GetAverageRatingForCyclists();
            return Ok(ratings);
        }

        // C.5.2: Get ratings for a specific cyclist
        [HttpGet("c5_2_ratings/cyclist/{cyclistId}")]
        public IActionResult GetRatingsForCyclist(int cyclistId)
        {
            var ratings = _queryService.GetRatingsForCyclist(cyclistId);
            return Ok(ratings);
        }

        // C.6: Get the monthly hours and earnings for a cyclist
        [HttpGet("c6_cyclist/{cyclistId}/earnings")]
        [Authorize(Policy = "AdminOrCyclistOnly")]
        public IActionResult GetMonthlyHoursAndEarnings(int cyclistId)
        {
            // Retrieve the IsAdmin claim
            var isAdmin = User.HasClaim("IsAdmin", "true");

            if (!isAdmin)
            {
                // Retrieve the CyclistId claim as a string
                var userCyclistIdStr = User.FindFirstValue("CyclistId");

                if (string.IsNullOrEmpty(userCyclistIdStr) || !int.TryParse(userCyclistIdStr, out int userCyclistId))
                {
                    // CyclistId claim is missing or invalid
                    return Unauthorized("Cyclist information is missing or invalid.");
                }

                if (userCyclistId != cyclistId)
                {
                    // The cyclistId in the URL does not match the user's CyclistId
                    return Unauthorized("You are not authorized to view this data.");
                }
            }



            var earnings = _queryService.GetMonthlyHoursAndEarnings(cyclistId);

            if (earnings == null)
            {
                return NotFound();
            }

            return Ok(earnings);
        }

        // C.7: Get data on cyclists including delivery completion and average delivery time
        [HttpGet("c7_cyclists/data")]
        [Authorize(Policy = "AdminOrManagerOnly")]
        public IActionResult GetCyclistData()
        {
            var data = _queryService.GetCyclistData();
            return Ok(data);
        }



    }
}
