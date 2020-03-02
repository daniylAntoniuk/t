using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class AdminViewModel
    {
        public List<int> CountOrders { get; set; }
        public List<int> CountUserProfiles { get; set; }
        public int Users { get; set; }
        public double Money { get; set; }
        public int Orders { get; set; }
    }
}
