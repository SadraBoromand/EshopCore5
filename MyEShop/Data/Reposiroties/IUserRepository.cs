using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyEShop.Context;
using MyEShop.Models;

namespace MyEShop.Data.Reposiroties
{
    public interface IUserRepository
    {
        bool IsExistUserByEmail(string email);
        void AddUser(Users user);
        Users GetUserForLogin(string email,string password);
    }

    public class UserRepository : IUserRepository
    {
        private MyEShopContext _context;

        public UserRepository(MyEShopContext context)
        {
            _context = context;
        }
        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void AddUser(Users user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public Users GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
