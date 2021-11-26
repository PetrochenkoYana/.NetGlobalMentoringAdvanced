using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.BusinessLogic
{
    public interface ICartingService
    {
        Task<Cart> GetCartInfo(ObjectId cartId);
        Task<IEnumerable<Item>> GetItemListAsync(ObjectId cartId);
        Task AddItemAsync(ObjectId cartId, Item item);
        Task RemoveItemAsync(ObjectId cartId, ObjectId itemId);
        Task<Cart> CreateAsync(IEnumerable<Item> items);
    }
}
