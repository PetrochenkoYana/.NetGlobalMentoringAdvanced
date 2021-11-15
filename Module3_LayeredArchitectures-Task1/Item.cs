using System.Runtime.InteropServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Module3_LayeredArchitectures_Task1
{
    public class Item
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("image")]
        public string Image { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
        [BsonElement("quantity")]
        public int Quantity { get; set; }

        public Item()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
