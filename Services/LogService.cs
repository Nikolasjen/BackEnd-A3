using FoodAppG4.LoggingLevels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodAppG4.Services
{
    public class LogService
    {
        private readonly IMongoCollection<LogEntry> _logsCollection;

        public LogService(IMongoClient client)
        {
            var database = client.GetDatabase("FoodAppLogs");
            _logsCollection = database.GetCollection<LogEntry>("log");
        }

        public async Task<List<LogEntry>> SearchLogsAsync(
            string? user,
            string? operation,
            DateTime? startDate,
            DateTime? endDate)
        {
            var filterBuilder = Builders<LogEntry>.Filter;
            var filters = new List<FilterDefinition<LogEntry>>();

            // Filter by user if provided (case-insensitive)
            if (!string.IsNullOrEmpty(user))
            {
                var regex = new BsonRegularExpression($"^{Regex.Escape(user)}$", "i"); // ^ and $ for exact match
                filters.Add(filterBuilder.Regex("Properties.User", regex));
            }

            // Filter by operation if provided (case-insensitive)
            if (!string.IsNullOrEmpty(operation))
            {
                var regex = new BsonRegularExpression($"^{Regex.Escape(operation)}$", "i");
                filters.Add(filterBuilder.Regex("Properties.Operation", regex));
            }

            // Filter by start date if provided
            if (startDate.HasValue)
            {
                filters.Add(filterBuilder.Gte(x => x.UtcTimeStamp, startDate.Value));
            }

            // Filter by end date if provided
            if (endDate.HasValue)
            {
                filters.Add(filterBuilder.Lte(x => x.UtcTimeStamp, endDate.Value));
            }

            // Combine all filters
            var combinedFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

            // Define a maximum limit to prevent excessively large responses
            const int maxLogs = 1000;

            // Execute the query with sorting and limit
            return await _logsCollection.Find(combinedFilter)
                                        .SortByDescending(x => x.UtcTimeStamp)
                                        .Limit(maxLogs)
                                        .ToListAsync();
        }
    }
}
