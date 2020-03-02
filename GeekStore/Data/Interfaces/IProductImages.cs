using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Interfaces
{
    public interface IProductImages
    {
        IEnumerable<ProductImage> ProductImages { get; }
        IEnumerable<ProductImage> GetImagesById(int productId);
    }
}
