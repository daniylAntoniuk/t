using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.EFContext
{
    public class DbRole:IdentityRole<string>
    {
        public IEnumerable<DbUserRole> UserRole { get; set; }
    }
}
