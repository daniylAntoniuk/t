using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GeekStore.Models;
using GeekStore.Data.Interfaces;
using GeekStore.Data.ViewModels;

namespace GeekStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubcategory _subcategory;
        private readonly ICategory _category;
        private readonly ICart _cart;
        private readonly IProduct _product;
        public HomeController(ISubcategory subcategory, ICategory category,IProduct product, ICart cart)
        {
            _subcategory = subcategory;
            _category = category;
            _product = product;
            _cart = cart;
        }
        public ViewResult AllDone()
        {
            return View();
        }
        public ViewResult AboutUs()
        {
            return View();
        }
        public  ViewResult Index()
        {
            List<CategoryViewModel> categories=new List<CategoryViewModel>();
            foreach (var item in _category.Categories)
            {
                categories.Add(new CategoryViewModel()
                {
                    Category = item,
                    Subcategories=_subcategory.Subcategories.Where(x=>x.CategoryId==item.Id)
                }) ;

            }
            HomeViewModel home = new HomeViewModel()
            {
                categories = categories,
                products = _product.ProductsAdv.ToList(),
                
            };
           
            
            return View(home);
        }
       
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
