using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class CategoryRepository:ICategory
    {
        private readonly DBContext _context;
        public CategoryRepository(DBContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Categories
        {
            get
            {
                return _context.Categories;
            }
        }
    }
}
