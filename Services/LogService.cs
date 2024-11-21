using FoodAppG4.LoggingLevels;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
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

        public async Task<List<LogEntry>> GetAsync()
        {
            // Get all log entries​
            var filter = Builders<LogEntry>.Filter.Empty;
            var count = await _logsCollection.CountDocumentsAsync(filter);
            Console.WriteLine("Number of documents found: " + count);
            return await _logsCollection.Find(filter).ToListAsync();
        }

        public async Task<List<LogEntry>> GetAsync(string operation)
        {
            // Build filter for Level "Information" and matching Operation
            var filterBuilder = Builders<LogEntry>.Filter;
            var filter = filterBuilder.Eq(x => x.Level, "Information") &
                         filterBuilder.Eq("Properties.Operation", operation);

            return await _logsCollection.Find(filter).ToListAsync();
        }

        // Optional: Additional methods for more flexible queries
        public async Task<List<LogEntry>> GetAsync(string operation, string level)
        {
            var filterBuilder = Builders<LogEntry>.Filter;
            var filter = filterBuilder.Eq(x => x.Level, level) &
                         filterBuilder.Eq("Properties.Operation", operation);

            return await _logsCollection.Find(filter).ToListAsync();
        }
    }
}
