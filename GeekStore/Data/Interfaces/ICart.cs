using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Interfaces
{
    public interface ICart
    {
       IEnumerable<Cart> Carts { get; }
        void AddProductToCart(int cartId, int productId);
        void AddCart(string userId);
        IEnumerable<ProductCart> ProductCarts { get; }
    }
}
