using GeekStore.Data.EFContext;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class EditProfileViewModel
    {
        public DbUser User { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<Order> Orders { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Sity { get; set; }
        public int  PostDepartment { get; set; }
    }
}
