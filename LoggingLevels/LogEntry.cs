using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace FoodAppG4.LoggingLevels
{
    [BsonIgnoreExtraElements]
    public class LogEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Level")]
        public string Level { get; set; } = "";

        [BsonElement("UtcTimeStamp")]
        public DateTime UtcTimeStamp { get; set; }

        [BsonElement("Properties")]
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public bool LevelEquals(string level)
        {
            return string.Equals(Level, level, StringComparison.OrdinalIgnoreCase);
        }
    }
}
