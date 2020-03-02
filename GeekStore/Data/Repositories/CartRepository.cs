using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class CartRepository : ICart
    {
        private readonly DBContext _context;
        public CartRepository(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Cart> Carts
        {
            get
            {
                return _context.Carts;
            }
        }

        public IEnumerable<ProductCart> ProductCarts
        {
            get
            {
                return _context.ProductCarts;
            }
        }

        public void AddCart(string userId)
        {
            _context.Carts.Add(new Cart()
            {
                UserId = userId
            });
            _context.SaveChanges();
        }

        public void AddProductToCart(int cartId, int productId)
        {
            _context.ProductCarts.Add(new ProductCart()
            {
                CartId = cartId,
                ProductId = productId
            });
            _context.SaveChanges();
        }
    }
}

