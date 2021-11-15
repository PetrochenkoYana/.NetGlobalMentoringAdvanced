using System.Collections.Generic;
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

        public Cart GetCartInfo(ObjectId cartId)
        {
            return _cartingRepository.GetCartInfo(cartId);
        }

        public IEnumerable<Item> GetItemList(ObjectId cartId)
        {
            return _cartingRepository.GetItemList(cartId);
        }

        public void AddItem(ObjectId cartId, Item item)
        {
            _cartingRepository.AddItem(cartId, item);
        }

        public void RemoveItem(ObjectId cartId, ObjectId itemId)
        {
            _cartingRepository.RemoveItem(cartId, itemId);
        }

        public Cart Create(IEnumerable<Item> items)
        {
            return _cartingRepository.Create(items);
        }
    }
}
