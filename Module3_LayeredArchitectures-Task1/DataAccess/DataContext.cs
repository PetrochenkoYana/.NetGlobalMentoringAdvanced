using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Cart> GetCartInfoAsync(ObjectId cartId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var collection = _database.GetCollection<Cart>("Carts");
            var carts = await collection.FindAsync(filter);
            return carts.FirstOrDefault();
        }

        public async Task<ICollection<Item>> GetItemListAsync(ObjectId cartId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var collection = _database.GetCollection<Cart>("Carts");
            var carts = await collection.FindAsync(filter);
            return carts.FirstOrDefault()?.Items;
        }

        public async Task AddItem(ObjectId cartId, Item item)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var update = Builders<Cart>.Update.AddToSet("items", item);
            await _database.GetCollection<Cart>("Carts").UpdateOneAsync(filter, update);
        }

        public async Task RemoveItem(ObjectId cartId, ObjectId itemId)
        {
            var filter = Builders<Cart>.Filter.Eq("_id", cartId);
            var filterItems = Builders<Cart>.Filter.Eq("_id", itemId);
            var delete = Builders<Cart>.Update.PullFilter("items", filterItems);
            await _database.GetCollection<Cart>("Carts").UpdateOneAsync(filter, delete);
        }

        public async Task<Cart> CreateAsync(IEnumerable<Item> items)
        {
            var cart = new Cart() { Items = items.ToList() };
            await _database.GetCollection<Cart>("Carts").InsertOneAsync(cart);
            return cart;
        }
    }
}
