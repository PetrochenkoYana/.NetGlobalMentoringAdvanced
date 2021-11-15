using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Module3_LayeredArchitectures_Task1
{
    public class Cart
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("items")]
        public List<Item> Items { get; set; }
    }
}
