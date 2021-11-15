using System;
using System.Collections.Generic;
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

        public Cart GetCartInfo(ObjectId cartId)
        {
            return _dataContext.GetCartInfo(cartId);
        }

        public IEnumerable<Item> GetItemList(ObjectId cartId)
        {
            return _dataContext.GetItemList(cartId);
        }

        public void AddItem(ObjectId cartId, Item item)
        {
            _dataContext.AddItem(cartId, item);
        }

        public void RemoveItem(ObjectId cartId, ObjectId itemId)
        {
            _dataContext.RemoveItem(cartId, itemId);
        }

        public Cart Create(IEnumerable<Item> items)
        {
            return _dataContext.Create(items);
        }
    }
}
