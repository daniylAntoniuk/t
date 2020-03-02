using GeekStore.Data.EFContext;
using GeekStore.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Components
{
    public class Search : ViewComponent
    {
        private readonly DBContext _context;
        public Search(DBContext context)
        {
            _context = context;
        }
        //[HttpPost]
        public IViewComponentResult Invoke()
        {
            //return View(_context.Products.Where(x => x.Name.Contains(model.Name)));
            return View();
        }
    }
}
