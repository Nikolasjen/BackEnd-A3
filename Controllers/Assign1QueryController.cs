using FoodAppG4.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppG4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Assign1QueryController : ControllerBase
    {
        private readonly QueryService _queryService;

        public Assign1QueryController(QueryService queryService)
        {
            _queryService = queryService;
        }

        // C.1: Get data for each cook
        [HttpGet("c1_cooks")]
        public IActionResult GetCookData()
        // public ActionResult<IEnumerable<dynamic>> GetCookData()
        {
            var cooks = _queryService.GetCookData();
            return Ok(cooks);
        }

        // C.2: Get available dish details for a specific cook's kitchen
        [HttpGet("c2_dishes/{cookId}")]
        public IActionResult GetDishDetailsForCook(int cookId)
        {
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
        public IActionResult GetCookAverageRating(int cookId)
        {
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
        public IActionResult GetMonthlyHoursAndEarnings(int cyclistId)
        {
            var earnings = _queryService.GetMonthlyHoursAndEarnings(cyclistId);
            return Ok(earnings);
        }

        // C.7: Get data on cyclists including delivery completion and average delivery time
        [HttpGet("c7_cyclists/data")]
        public IActionResult GetCyclistData()
        {
            var data = _queryService.GetCyclistData();
            return Ok(data);
        }



    }
}