using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class OrderViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool IsLikeSelected { get; set; }
        public bool IsDislikeSelected { get; set; }

        public string CommentText { get; set; }
        public string UserId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        
        [Required]
        public int PostDepartament { get; set; }
        [Required]
        public string Sity { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
