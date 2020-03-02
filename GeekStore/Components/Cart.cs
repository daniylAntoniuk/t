using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using GeekStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Components
{
    public class Cart:ViewComponent
    {
        
        public readonly ICart _cart;
        public Cart(ICart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            int count = 0;
            var info = HttpContext.Session.GetString("SessionUserData");
            if (info != null)
            {
                var res = JsonConvert.DeserializeObject<UserInfo>(info);               
                count =  _cart.ProductCarts.Where(x => x.CartId==_cart.Carts.FirstOrDefault(y => y.UserId == res.UserId).Id).ToList().Count;
            }
            return View(count);
        }
        
    }
}
