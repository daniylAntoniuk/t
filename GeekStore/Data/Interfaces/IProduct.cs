using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Interfaces
{
    public interface IProduct
    {
        IEnumerable<Product> Products { get; }
         IEnumerable<Product> ProductsAdm { get; }
        IEnumerable<Product> ProductsAdv { get; }

        Product GetProductById(int id);
        /// <summary>
        /// Returns products with subcategory id such as you write
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        IEnumerable<Product> GetProductsBySubcategory(int subcategoryId);
        void Enable(int id);
        void Disable(int id);
    }
}
