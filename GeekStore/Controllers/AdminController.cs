using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IOrder _order;
        private readonly IProduct _product;
        private readonly IUserProfile _userProfile;
        private readonly DBContext _context;
        public AdminController(DBContext context,IOrder order, IProduct product, IUserProfile userProfile)
        {
            _order = order;
            _product = product;
            _userProfile = userProfile;
            _context = context;
        }
        public ViewResult Login()
        {
            return View();
        }

        public ViewResult Home()
        {
            double money = 0;
            List<int> counts=new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                int count = 0;
                foreach (var el in _order.Orders)
                {
                    if (el.OrderDate.Month == i)
                    {
                        count += 1;
                    }
                }
                counts.Add(count);
            }
            List<int> counts2 = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                int count = 0;
                foreach (var el in _userProfile.UserProfiles)
                {
                    if (el.RegisterDate.Month == i)
                    {
                        count += 1;
                    }
                }
                counts2.Add(count);
            }
            foreach(var el in _order.Orders)
            {
                money+=_product.GetProductById(el.ProductId).Price;
                
            }
            return View(new AdminViewModel()
            {
                CountOrders=counts,
                CountUserProfiles=counts2,
                Orders=_order.Orders.Count(),
                Users=_context.Users.Count(),
                Money=money
            }
            );
        }

        public ViewResult Products()
        {
            return View(_product.ProductsAdm);
        }
        public ViewResult AddProduct()
        {
            return View(_product.Products);
        }
        public ViewResult Orders()
        {

            return View(_order.OrdersAdm);
        }

        [Route("Admin/OrderSent/{id}")]
        public IActionResult OrderSent(int id)
        {
            _order.Sent(id);
            return RedirectToAction("Orders", "Admin");
        }
        [Route("Admin/ProductDisable/{id}")]
        public IActionResult ProductDisable(int id)
        {
            _product.Disable(id);
            return RedirectToAction("Products", "Admin");
        }
        [Route("Admin/ProductEnable/{id}")]
        public IActionResult ProductEnable(int id)
        {
            _product.Enable(id);
            return RedirectToAction("Products", "Admin");
        }
    }
}