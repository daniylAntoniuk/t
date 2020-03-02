using GeekStore.Data.EFContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Tables
{
    public class UserProfile
    {
        /// <summary>
        /// id
        /// </summary>
        [Key,ForeignKey("User")]
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Image { get; set; }
        public int PostDepartament { get; set; }
        public string Sity { get; set; }
        public DateTime RegisterDate { get; set; }
        public virtual DbUser User { get; set; }
    }
}
