using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.Data;
using MyEShop.Models;

namespace MyEShop.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly MyEShopContext _context;

        public IndexModel(MyEShopContext context)
        {
            _context = context;
        }

        public IList<Users> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
