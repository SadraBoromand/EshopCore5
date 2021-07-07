using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyEShop.Context;
using MyEShop.Models;

namespace MyEShop.Data.Reposiroties
{
   public interface IGroupRepository
   {
       IEnumerable<Category> GetAllCategories();
       IEnumerable<ShowGroupViewModel> GetGroupForShow();
   }

   public class GroupRepository:IGroupRepository
   {
       private MyEShopContext _context;
       public GroupRepository(MyEShopContext context)
       {
           _context = context;
       }

       public IEnumerable<Category> GetAllCategories()
       {
           return _context.Cagegories;
       }

       public IEnumerable<ShowGroupViewModel> GetGroupForShow()
       {
           return _context.Cagegories
               .Select(c => new ShowGroupViewModel()
               {
                   GroupId = c.CategoryID,
                   Name = c.Name,
                   ProductCount = _context.CategoryToProducts.Count(g => g.CategoryId == c.CategoryID)
               })
               .ToList();
        }
   }
}
