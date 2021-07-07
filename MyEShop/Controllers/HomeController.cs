using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyEShop.Context;
using MyEShop.ViewModels;

namespace MyEShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyEShopContext _context;
        private static Cart _cart = new Cart();


        public HomeController(ILogger<HomeController> logger, MyEShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context
                .Products
                .ToList();
            return View(products);
        }

        [Route("{id}/{name}")]
        public IActionResult Detail(string name, int id)
        {
            var product = _context
                .Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.ProductID == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(ca => ca.Category)
                .ToList();

            var vm = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(vm);
        }

        [Authorize]
        [Route("/Home/AddToCart/{itemId}")]
        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.ItemId == itemId);
            if (product != null)
            {
                //var cartItem = new CartItem()
                //{
                //    Item = product.Item,
                //    Quantity = 1
                //};
                //_cart.AddItem(cartItem);
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFainaly);
                if (order != null)
                {
                    var orderDetail = _context.OrderDatails.FirstOrDefault(d =>
                        d.OrderId == order.OrderId && d.ProductId == product.ProductID);
                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;
                    }
                    else
                    {
                        _context.OrderDatails.Add(new OrderDatail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductID,
                            Price = product.Item.Price,
                            Count = 1
                        });
                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFainaly = false,
                        CreateDate = DateTime.Now,
                        UserId = userId
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.OrderDatails.Add(new OrderDatail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductID,
                        Price = product.Item.Price,
                        Count = 1
                    });
                }

                _context.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        [Route("ShowCart")]
        public IActionResult ShowCart()
        {
            //var CartVM = new CartViewModel()
            //{
            //    CartItems = _cart.CartItems,
            //    OrderTotal = _cart.CartItems.Sum(c => c.GetTotalPrice())
            //};
            //return View(CartVM);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.Where(o => o.UserId == userId && !o.IsFainaly)
                .Include(o => o.OrderDatails)
                .ThenInclude(o => o.Product)
                .FirstOrDefault();
            return View(order);
        }

        [Authorize]
        [Route("RemoveCart/{detailId}")]
        public IActionResult RemoveCart(int detailId)
        {
            //_cart.RemoveItem(itemId);
            var orderDetail = _context.OrderDatails.Find(detailId);
            _context.Remove(orderDetail);
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        [Route("/ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
