using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Models;

namespace MyEShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyEShopContext _context;

        public IndexModel(MyEShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products.Include(p => p.Item);
        }

        public void OnPost()
        {
        }
    }
}
