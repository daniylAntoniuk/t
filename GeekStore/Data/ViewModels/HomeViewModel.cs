using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class HomeViewModel
    {
        public List<CategoryViewModel> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
        public int cartCount { get; set; }
    }
}
