using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public bool IsDisabled { get; set; }
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        public int Discount { get; set; }
       
        [ForeignKey("SubcategoryOf")]
        public int SubcategoryId { get; set; }
        public virtual Subcategory SubcategoryOf { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }
    }
}
