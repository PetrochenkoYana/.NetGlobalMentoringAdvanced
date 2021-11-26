using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.DataAccess
{
    public interface ICartingRepository
    {
        Task<Cart> GetCartInfoAsync(ObjectId cartId);
        Task<IEnumerable<Item>> GetItemListAsync(ObjectId cartId);
        Task AddItemAsync(ObjectId cartId, Item item);
        Task RemoveItemAsync(ObjectId cartId, ObjectId itemId);
        Task<Cart> CreateAsync(IEnumerable<Item> items);
    }
}
