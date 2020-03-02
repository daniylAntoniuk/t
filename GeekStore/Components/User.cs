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
    public class User:ViewComponent
    {
        
        

        public IViewComponentResult Invoke()
        {
            UserInfo user = new UserInfo()
            {
                Email = "",
                UserId = ""
            };
            var info = HttpContext.Session.GetString("SessionUserData");
            if (info != null)
            {
                user.UserId = JsonConvert.DeserializeObject<UserInfo>(info).UserId;
            }
            return View(user);
        }
    }
}
