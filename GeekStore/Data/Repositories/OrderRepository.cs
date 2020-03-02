using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly DBContext _context;
        public OrderRepository(DBContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> OrdersAdm
        {
            get
            {
                return _context.Orders.Where(x=>x.Sent!=true);
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return _context.Orders;
            }
        }

        public void Sent(int orderId)
        {
            _context.Orders.FirstOrDefault(x => x.Id == orderId).Sent=true;
            _context.SaveChanges();
        }
    }
}
