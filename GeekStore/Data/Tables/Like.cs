using GeekStore.Data.EFContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DbUserOf")]
        public string UserId { get; set; }
        public virtual DbUser DbUserOf { get; set; }

        [ForeignKey("ProductOf")]
        public int ProductId { get; set; }
        public virtual Product ProductOf { get; set; }
    }
}
