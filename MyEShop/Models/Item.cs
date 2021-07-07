using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStack { get; set; }


        // Navigation Property
        public Product Product { get; set; }
    }
}
