using System.Collections.Generic;
using MongoDB.Bson;

namespace Module3_LayeredArchitectures_Task1.DataAccess
{
    public interface IDataContext
    {
        Cart GetCartInfo(ObjectId cartId);
        ICollection<Item> GetItemList(ObjectId cardId);
        void AddItem(ObjectId cartId, Item item);
        void RemoveItem(ObjectId cartId, ObjectId itemId);
        Cart Create(IEnumerable<Item> items);

    }
}
