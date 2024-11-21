using MongoDB.Bson.Serialization.Attributes;

namespace FoodAppG4.LoggingLevels
{
    public class LogInfo
    {
        [BsonElement("Operation")]
        public string Operation { get; set; } = "";

        public string User { get; set; } = "";
    }
}