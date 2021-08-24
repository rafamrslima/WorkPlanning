using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace WorkPlanning.Domain.Entities
{
    public class Worker
    {
        public Worker(string name, string personalId)
        {
            Validate(name, personalId);
            Name = name;
            PersonalId = personalId;
            Shifts = new List<Shift>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; private set; }

        public string PersonalId { get; private set; }

        public List<Shift> Shifts { get; set; } 

        private static void Validate(string name, string personalId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can not be empty.");

            if (name.Length < 3)
                throw new ArgumentException("Name should be at least 3 characters long.");

            if (string.IsNullOrWhiteSpace(personalId))
                throw new ArgumentException("Personal Id can not be empty.");
        }
    }
}
