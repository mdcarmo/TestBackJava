﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ExpenseManagement.Core.Entities
{
    public class Spent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }
        public long CodeUser { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public DateTime PostedAt { get; set; }
        public string? Category { get; set; }
    }
}