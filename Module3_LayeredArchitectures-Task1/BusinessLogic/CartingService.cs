using System.Collections.Generic;
using System.Threading.Tasks;
using Module3_LayeredArchitectures_Task1.DataAccess;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.BusinessLogic
{
    public class CartingService : ICartingService
    {
        private readonly ICartingRepository _cartingRepository;

        public CartingService(ICartingRepository cartingRepository)
        {
            _cartingRepository = cartingRepository;
        }

        public async Task<Cart> GetCartInfo(ObjectId cartId)
        {
            return await _cartingRepository.GetCartInfoAsync(cartId);
        }

        public async Task<IEnumerable<Item>> GetItemListAsync(ObjectId cartId)
        {
            return await _cartingRepository.GetItemListAsync(cartId);
        }

        public async Task AddItemAsync(ObjectId cartId, Item item)
        {
            await _cartingRepository.AddItemAsync(cartId, item);
        }

        public async Task RemoveItemAsync(ObjectId cartId, ObjectId itemId)
        {
            await _cartingRepository.RemoveItemAsync(cartId, itemId);
        }

        public async Task<Cart> CreateAsync(IEnumerable<Item> items)
        {
            return await _cartingRepository.CreateAsync(items);
        }
    }
}
