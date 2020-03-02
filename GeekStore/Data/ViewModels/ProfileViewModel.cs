using GeekStore.Data.EFContext;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class ProfileViewModel
    {
        public DbUser User { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
