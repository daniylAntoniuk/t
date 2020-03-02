using GeekStore.Data.Tables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.EFContext
{
    public class DbUser:IdentityUser<string>
    {
        public IEnumerable<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        // public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }
    }
}
