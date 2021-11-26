using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.DataAccess
{
    public class CartingRepository : ICartingRepository
    {
        private readonly IDataContext _dataContext;

        public CartingRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Cart> GetCartInfoAsync(ObjectId cartId)
        {
            return await _dataContext.GetCartInfoAsync(cartId);
        }

        public async Task<IEnumerable<Item>> GetItemListAsync(ObjectId cartId)
        {
            return await _dataContext.GetItemListAsync(cartId);
        }

        public async Task AddItemAsync(ObjectId cartId, Item item)
        {
            await _dataContext.AddItem(cartId, item);
        }

        public async Task RemoveItemAsync(ObjectId cartId, ObjectId itemId)
        {
            await _dataContext.RemoveItem(cartId, itemId);
        }

        public async Task<Cart> CreateAsync(IEnumerable<Item> items)
        {
            return await _dataContext.CreateAsync(items);
        }
    }
}
