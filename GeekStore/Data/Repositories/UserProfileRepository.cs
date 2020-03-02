using GeekStore.Data.EFContext;
using GeekStore.Data.Interfaces;
using GeekStore.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.Repositories
{
    public class UserProfileRepository : IUserProfile
    {
        private readonly DBContext _context;
        public UserProfileRepository(DBContext context)
        {
            _context = context;
        }
        public IEnumerable<UserProfile> UserProfiles
        {
            get
            {
                return _context.UserProfiles;
            }
        }
    }
}
