using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WorkPlanning.Domain.Entities
{
    public class Shift
    {
        public Shift(DateTime startTime, string workerId)
        {
            Validate(startTime); 
            StartTime = startTime;
            EndTime = startTime.AddHours(8);
            Id = ObjectId.GenerateNewId().ToString();
            WorkerId = workerId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string WorkerId { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        private static void Validate(DateTime startTime)
        {
            if(startTime == DateTime.MinValue)
                throw new ArgumentException("Start time should be defined.");
        }
    }
}
