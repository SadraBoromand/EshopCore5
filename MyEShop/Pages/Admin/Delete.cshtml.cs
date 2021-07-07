using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Models;

namespace MyEShop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private MyEShopContext _context;

        public DeleteModel(MyEShopContext context)
        {
            _context = context;
        }

        [BindProperty] 
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products.FirstOrDefault(p => p.ProductID == id);

        }

        public IActionResult OnPost()
        {
            var product = _context.Products.Find(Product.ProductID);
            var item = _context.Items.First(p => p.ItemID == product.ItemId);
            _context.Items.Remove(item);
            _context.Products.Remove(product);
            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Images");
            if (!System.IO.Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath += "/" + product.ProductID + ".jpg";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            return RedirectToPage("Index");
        }
    }
}
