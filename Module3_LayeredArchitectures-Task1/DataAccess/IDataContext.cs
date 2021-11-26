using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.DataAccess
{
    public interface IDataContext
    {
        Task<Cart> GetCartInfoAsync(ObjectId cartId);
        Task<ICollection<Item>> GetItemListAsync(ObjectId cardId);
        Task AddItem(ObjectId cartId, Item item);
        Task RemoveItem(ObjectId cartId, ObjectId itemId);
        Task<Cart> CreateAsync(IEnumerable<Item> items);

    }
}
