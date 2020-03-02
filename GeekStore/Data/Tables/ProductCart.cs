using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class ProductCart
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }

        [ForeignKey("CartOf")]
        public int CartId { get; set; }
        public virtual Cart CartOf { get; set; }

        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }
        public virtual Product ProductOf { get; set; }
    }
}
