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
    public class EditModel : PageModel
    {
        private MyEShopContext _context;

        public EditModel(MyEShopContext context)
        {
            _context = context;
        }

        [BindProperty] public AddEditProductViewModel Product { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products
                .Include(p => p.Item)
                .Where(p => p.ProductID == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.ProductID,
                    Description = s.Description,
                    Name = s.Name,
                    QuantityInStack = s.Item.QuantityInStack,
                    Price = s.Item.Price
                })
                .FirstOrDefault();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(p => p.ItemID == product.ItemId);

            product.Name = Product.Name;
            product.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStack = Product.QuantityInStack;

            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "Images");
                if (!System.IO.Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath += "/" + product.ProductID + Path.GetExtension(Product.Picture.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
