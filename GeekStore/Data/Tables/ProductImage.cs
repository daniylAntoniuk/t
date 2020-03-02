using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string ImgName { get; set; }
        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }
        public virtual Product ProductOf { get; set; }
    }
}
