using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class Order
    { 
        [Key]
        public int Id { get; set; }
        public string Sity { get; set; }
        public int PostDepartament { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public int Count { get; set; }
        public DateTime OrderDate { get; set; }
        
        public bool Sent { get; set; }
        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }
        public virtual Product ProductOf { get; set; }

        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}
