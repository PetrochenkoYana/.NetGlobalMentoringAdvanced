using System.Collections.Generic;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.BusinessLogic
{
    public interface ICartingService
    {
        Cart GetCartInfo(ObjectId cartId);
        IEnumerable<Item> GetItemList(ObjectId cartId);
        void AddItem(ObjectId cartId, Item item);
        void RemoveItem(ObjectId cartId, ObjectId itemId);
        Cart Create(IEnumerable<Item> items);
    }
}
