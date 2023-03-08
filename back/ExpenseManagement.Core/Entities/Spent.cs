using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace ExpenseManagement.Core.Entities
{
    public  class Spent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        /// <param name="postedAt"></param>
        /// <param name="category"></param>
        public Spent(long codeUser, string description, double value, DateTime postedAt, string category)
        {
            CodeUser = codeUser;
            Description = description;
            Value = value;
            PostedAt = postedAt;
            Category = category;
        }


        [BsonId]
        //[BsonRepresentation(BsonType.String)]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        //public Guid Id { get; set; }
        public string Id { get; set; }
        public long CodeUser { get; private set; }
        public string? Description { get; private set; }
        public double Value { get; private set; }
        public DateTime PostedAt { get; private set; }
        public string? Category { get; set; }
    }
}
