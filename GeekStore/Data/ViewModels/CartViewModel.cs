using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<ProductCart> ProductCarts { get; set; }
        public string UserId { get; set; }
    }
}
