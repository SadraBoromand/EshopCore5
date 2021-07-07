using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEShop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        // Navigation Property
        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
    }
}
