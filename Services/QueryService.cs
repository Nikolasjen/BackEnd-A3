using FoodAppG4.Data;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodAppG4.Services
{
    public class QueryService
    {
        private readonly FoodAppG4Context _context;

        public QueryService(FoodAppG4Context context)
        {
            _context = context;
        }

        // C.1: Get data for each cook
        public IEnumerable<dynamic> GetCookData()
        {
            return _context.Cooks
                .Select(c => new
                {
                    c.Name,
                    c.Address,
                    PhoneNumber = c.Phone,
                    PassedFoodSafetyCourse = c.PassedCourse
                })
                .ToList();
        }

        // C.2: Get available dish details for a specific cook's kitchen
public IEnumerable<dynamic> GetDishDetailsForCook(int cookId)
{
    return _context.Dishes
        .Join(_context.OrderDetails,
              dish => dish.DishId,
              orderDetail => orderDetail.DishId,
              (dish, orderDetail) => new { dish, orderDetail })
        .Where(joined => joined.dish.CookId == cookId)
        .AsEnumerable() // Switch to LINQ-to-Objects to enable the use of .ToString with formatting
        .Select(joined => new
        {
            DishName = joined.dish.Name,
            joined.orderDetail.Quantity,
            joined.dish.Price,
            AvailableFrom = joined.dish.AvailableFrom.HasValue ? joined.dish.AvailableFrom.Value.ToString("dd-MM-yyyy HH:mm") : "N/A", // Format the DateTime if it's not null
            AvailableTo = joined.dish.AvailableTo.HasValue ? joined.dish.AvailableTo.Value.ToString("dd-MM-yyyy HH:mm") : "N/A" // Format the DateTime if it's not null
        })
        .ToList();
}



        // C.3: Get list of dishes and kitchen for an order
        public IEnumerable<dynamic> GetOrderDetails(int orderId)
        {
            return _context.OrderDetails
                .Join(_context.Dishes,
                      orderDetail => orderDetail.DishId,
                      dish => dish.DishId,
                      (orderDetail, dish) => new { orderDetail, dish })
                .Join(_context.Cooks,
                      joined => joined.dish.CookId,
                      cook => cook.CookId,
                      (joined, cook) => new { joined.orderDetail, joined.dish, cook })
                .Join(_context.Orders,
                      joined => joined.orderDetail.OrderId,
                      order => order.OrderId,
                      (joined, order) => new { joined.orderDetail, joined.dish, joined.cook, order })
                .Where(joined => joined.orderDetail.OrderId == orderId)
                .Select(joined => new
                {
                    DishName = joined.dish.Name,
                    joined.orderDetail.Quantity,
                    KitchenName = joined.cook.Name,
                    OrderDate = joined.order.OrderDate
                })
                .ToList();
        }

        // C.4: Get the trip details for a cyclist
        public IEnumerable<dynamic> GetTripDetails(int tripId)
        {
            return _context.TripStops
                .Join(_context.Trips,
                      tripStop => tripStop.TripId,
                      trip => trip.TripId,
                      (tripStop, trip) => new { tripStop, trip })
                .Join(_context.Cyclists,
                      joined => joined.trip.CyclistId,
                      cyclist => cyclist.CyclistId,
                      (joined, cyclist) => new { joined.tripStop, cyclist })
                .Where(joined => joined.tripStop.TripId == tripId)
                .AsEnumerable()
                .Select(joined => new
                {
                    Responsible = joined.cyclist.Name,
                    CurrentAddress = joined.tripStop.StopAddress,
                    StatusDateTime = joined.tripStop.StopTime.HasValue ? joined.tripStop.StopTime.Value.ToString("dd-MM-yyyy HH:mm") : "N/A", // Format the DateTime if it's not null
                    CurrentStatus = joined.tripStop.ActionType
                })
                .ToList();
        }

        // C.5: Get the average rating for a cook
        public dynamic? GetCookAverageRating(int cookId)
        {
            var ratings = _context.Ratings
                .Where(r => r.CookId == cookId)
                .GroupBy(r => r.CookId)
                .Select(g => new
                {
                    AverageDeliveryRating = g.Average(r => r.DeliveryScore),
                    AverageFoodRating = g.Average(r => r.FoodScore),
                    OverallAverageRating = g.Average(r => (r.DeliveryScore + r.FoodScore) / 2.0)
                })
                .FirstOrDefault();

            return ratings;
        }

        // C.5.1: Get the average rating for all cyclists
        public IEnumerable<dynamic> GetAverageRatingForCyclists()
        {
            return _context.Ratings
                .Join(_context.Cyclists,
                      rating => rating.CyclistId,
                      cyclist => cyclist.CyclistId,
                      (rating, cyclist) => new { rating, cyclist })
                .GroupBy(joined => joined.cyclist.Name)
                .Select(g => new
                {
                    CyclistName = g.Key,
                    AverageDeliveryRating = g.Average(x => x.rating.DeliveryScore),
                    AverageFoodRating = g.Average(x => x.rating.FoodScore),
                    OverallAverageRating = g.Average(x => (x.rating.DeliveryScore + x.rating.FoodScore) / 2.0)
                })
                .ToList();
        }

        // C.5.2: Get the ratings for a specific cyclist
        public IEnumerable<dynamic> GetRatingsForCyclist(int cyclistId)
        {
            return _context.Ratings
                .Join(_context.Customers,
                      rating => rating.CustomerId,
                      customer => customer.CustomerId,
                      (rating, customer) => new { rating, customer })
                .Join(_context.Cooks,
                      joined => joined.rating.CookId,
                      cook => cook.CookId,
                      (joined, cook) => new { joined.rating, joined.customer, cook })
                .Where(joined => joined.rating.CyclistId == cyclistId)
                .Select(joined => new
                {
                    joined.rating.RatingId,
                    joined.rating.DeliveryScore,
                    joined.rating.FoodScore,
                    AverageScore = (joined.rating.DeliveryScore + joined.rating.FoodScore) / 2.0,
                    CustomerName = joined.customer.Name,
                    CookName = joined.cook.Name
                })
                .ToList();
        }

        // C.6: Get the monthly hours and earnings for a cyclist
        public IEnumerable<dynamic> GetMonthlyHoursAndEarnings(int cyclistId)
        {
            var trips = _context.Trips
                .Where(t => t.CyclistId == cyclistId)
                .ToList();

            var pickupStops = _context.TripStops
                .Where(ts => ts.ActionType == "Picked Up" && trips.Select(t => t.TripId).Contains(ts.TripId ?? 0))
                .ToList();

            var deliveryStops = _context.TripStops
                .Where(ts => ts.ActionType == "Delivered" && trips.Select(t => t.TripId).Contains(ts.TripId ?? 0))
                .ToList();

            var monthlyData = pickupStops
                .Join(deliveryStops,
                      pickup => pickup.TripId,
                      delivery => delivery.TripId,
                      (pickup, delivery) => new
                      {
                          Month = pickup.StopTime.HasValue ? pickup.StopTime.Value.Month : 0,
                          Hours = (pickup.StopTime.HasValue && delivery.StopTime.HasValue)
                                  ? (delivery.StopTime.Value - pickup.StopTime.Value).TotalHours
                                  : 0
                      })
                .GroupBy(x => x.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalHours = g.Sum(x => x.Hours),
                    TotalEarnings = g.Count() * 100
                })
                .ToList();

            return monthlyData;
        }


        // C.7: Get data on cyclists, including delivery completion and average delivery time
        public IEnumerable<dynamic> GetCyclistData()
        {
            var trips = _context.Trips
                .Include(t => t.Cyclist)
                .ToList();

            var pickupStops = _context.TripStops
                .Where(ts => ts.ActionType == "Picked Up" && trips.Select(t => t.TripId).Contains(ts.TripId ?? 0))
                .ToList();

            var deliveryStops = _context.TripStops
                .Where(ts => ts.ActionType == "Delivered" && trips.Select(t => t.TripId).Contains(ts.TripId ?? 0))
                .ToList();

            var cyclistData = pickupStops
                .Join(deliveryStops,
                      pickup => pickup.TripId,
                      delivery => delivery.TripId,
                      (pickup, delivery) => new
                      {
                          CyclistName = trips.FirstOrDefault(t => t.TripId == pickup.TripId)?.Cyclist?.Name ?? "Unknown",
                          Month = pickup.StopTime.HasValue ? pickup.StopTime.Value.Month : 0,
                          DeliveryTime = (pickup.StopTime.HasValue && delivery.StopTime.HasValue)
                                         ? (delivery.StopTime.Value - pickup.StopTime.Value).TotalMinutes
                                         : 0
                      })
                .GroupBy(x => new { x.CyclistName, x.Month })
                .Select(g => new
                {
                    CyclistName = g.Key.CyclistName,
                    Month = g.Key.Month,
                    DeliveriesCompleted = g.Count(),
                    AvgDeliveryTimeMinutes = g.Average(x => x.DeliveryTime)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return cyclistData;
        }



    }
}
