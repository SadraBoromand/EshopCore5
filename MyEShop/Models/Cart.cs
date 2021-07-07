using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public int OrderID { get; set; }

        // Navigation Property
        public List<CartItem> CartItems { get; set; }

        public void AddItem(CartItem item)
        {
            if (CartItems.Exists(i => i.Item.ItemID == item.Item.ItemID))
            {
                CartItems
                    .Find(i => i.Item.ItemID == item.Item.ItemID)
                    .Quantity += 1;
            }
            else
            {
                CartItems.Add(item);
            }
        }

        public void RemoveItem(int itemId)
        {
            var item = CartItems
                .SingleOrDefault(c => c.Item.ItemID == itemId);
            if (item?.Quantity <= 1)
            {
                CartItems.Remove(item);
            }
            else if (item != null)
            {
                item.Quantity -= 1;
            }

        }
    }
}
