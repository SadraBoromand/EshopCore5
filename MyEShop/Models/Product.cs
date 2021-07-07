using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Models
{
    public class Product
    {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }

        // Navigation Property
        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public Item Item { get; set; }
        public List<OrderDatail> OrderDatails { get; set; }
    }
}
