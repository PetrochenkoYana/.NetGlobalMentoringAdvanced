using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Module3_LayeredArchitectures_Task1.DataAccess
{
    public class MongoDbDataContext : IDataContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbDataContext(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:MongoDb"];
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase("Catalog");
        }

        public Cart GetCartInfo(ObjectId cartId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var collection = _database.GetCollection<Cart>("Carts");
            return collection.Find(filter).ToListAsync().Result.FirstOrDefault(); ;
        }

        public ICollection<Item> GetItemList(ObjectId cartId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var collection = _database.GetCollection<Cart>("Carts");
            var cart = collection.Find(filter).ToListAsync().Result.FirstOrDefault();
            return cart?.Items;
        }

        public void AddItem(ObjectId cartId, Item item)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var update = Builders<Cart>.Update.AddToSet("items", item);
            _database.GetCollection<Cart>("Carts").UpdateOne(filter, update);
        }

        public void RemoveItem(ObjectId cartId, ObjectId itemId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var filterItems = Builders<Cart>.Filter.Eq("_id", itemId);
            var delete = Builders<Cart>.Update.PullFilter("items", filterItems);
            _database.GetCollection<Cart>("Carts").UpdateOne(filter, delete);
        }

        public Cart Create(IEnumerable<Item> items)
        {
            var cart = new Cart() { Items = items.ToList() };
            _database.GetCollection<Cart>("Carts").InsertOne(cart);
            return cart;
        }
    }
}
