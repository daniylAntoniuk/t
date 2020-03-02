using GeekStore.Data.Interfaces;
using GeekStore.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Components
{
    public class Category:ViewComponent
    {
        private readonly ISubcategory _subcategory;
        public readonly ICategory _category;
        public Category(ISubcategory subcategory,ICategory category)
        {
            _category = category;
            _subcategory = subcategory;
        }

        public IViewComponentResult Invoke()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            foreach (var item in _category.Categories)
            {
                categories.Add(new CategoryViewModel()
                {
                    Category = item,
                    Subcategories = _subcategory.Subcategories.Where(x => x.CategoryId == item.Id)
                });

            }
            


            return View(categories);
        }
    }
}
