using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class ProductImageRepository : IProductImages
    {
        private readonly DBContext _context;
        public ProductImageRepository(DBContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductImage> ProductImages
        {
            get
            {
                return _context.ProductImages;
            }
        }

        public IEnumerable<ProductImage> GetImagesById(int productId)
        {
            return _context.ProductImages.Where(x => x.ProductId == productId);
        }
    }
}
