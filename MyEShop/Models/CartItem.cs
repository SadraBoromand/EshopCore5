using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }

        // Navigation Property
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Item.Price * Quantity;
        }
    }
}
