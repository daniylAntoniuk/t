using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("CategoryOf")]
        public int CategoryId { get; set; }
        public virtual Category CategorOF { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
