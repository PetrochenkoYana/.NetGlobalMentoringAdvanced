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
        Cart GetCartInfo(ObjectId cartId);
        IEnumerable<Item> GetItemList(ObjectId cartId);
        void AddItem(ObjectId cartId, Item item);
        void RemoveItem(ObjectId cartId, ObjectId itemId);
        Cart Create(IEnumerable<Item> items);
    }
}
