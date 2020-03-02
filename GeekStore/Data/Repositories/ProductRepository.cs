using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class ProductRepository:IProduct
    {
        private readonly DBContext _context;
        public ProductRepository(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return _context.Products.Where(x => x.IsDisabled != true);
            }
        }
        public IEnumerable<Product> ProductsAdm
        {
            get
            {
                return _context.Products;
            }
        }
        public IEnumerable<Product> ProductsAdv
        {
            get
            {
                return _context.Products.Where(x => x.Discount != 0).Where(x=>x.IsDisabled!=true);
            }
        }

        public void Disable(int id)
        {
            _context.Products.FirstOrDefault(x => x.Id == id).IsDisabled = true;
            _context.SaveChanges();
        }
        public void Enable(int id)
        {
            _context.Products.FirstOrDefault(x => x.Id == id).IsDisabled = false;
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProductsBySubcategory(int subcategoryId)
        {
            return _context.Products.Where(x => x.SubcategoryId == subcategoryId).Where(x => x.IsDisabled != true);
        }
    }
}
