using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEShop.Context;
using MyEShop.Models;

namespace MyEShop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyEShopContext _context;

        public AddModel(MyEShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var item = new Item()
            {
                Price = Product.Price,
                QuantityInStack = Product.QuantityInStack
            };
            _context.Add(item);
            _context.SaveChanges();

            var pro = new Product()
            {
                Name = Product.Name,
                Item = item,
                Description = Product.Description
            };

            _context.Add(pro);
            _context.SaveChanges();
            pro.ItemId = pro.ProductID;
            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "Images");
                if (!System.IO.Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath += "/" + pro.ProductID + Path.GetExtension(Product.Picture.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
