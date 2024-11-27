using MongoDB.Bson.Serialization.Attributes;

namespace FoodAppG4.LoggingLevels
{
    public partial class LogProperties
    {
        [BsonElement("LogInfo")]
        public LogInfo logInfo { get; set; }
    }
    // }
}