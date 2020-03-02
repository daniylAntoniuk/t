using GeekStore.Data.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.EFContext
{
    public class Seed
    {
        
       
        public static  void SeedUsers(UserManager<DbUser> userManager,RoleManager<DbRole> roleManager,DBContext _context)
        {
            var roleName = "User";
            var roleName2 = "Admin";
            if (!_context.Roles.Any(r => r.Name == roleName)&& !_context.Roles.Any(r => r.Name == roleName2))
            {
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
                var result2 = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName2
                }).Result;
            }
            UserProfile profile = new UserProfile()
            {
                FirstName = "Vasyan",
                LastName = "Telesik"

            };
            UserProfile profile2 = new UserProfile()
            {
                FirstName = "e",
                LastName = "eeee"

            };
            UserProfile profile3 = new UserProfile()
            {
                FirstName = "rrrr",
                LastName = "eeeeee"

            };
            DbUser user = new DbUser()
            {
                UserName = "den4ik",
                Email = "danik22122005@gmail.com",
                PhoneNumber = "+380955226415",
                UserProfile=profile
            };
            DbUser user2 = new DbUser()
            {
                UserName = "vasya228",
                Email = "vasya228@gmail.com",
                PhoneNumber = "+380985848448",
                UserProfile = profile2
            };
            DbUser user3 = new DbUser()
            {
                UserName = "ivasuk-telesuk",
                Email = "ivasuk-telesuk@ukr.net",
                PhoneNumber = "+380985845456",
                
                UserProfile = profile3
            };

            if (!_context.Users.Any(r => r.Email == user.Email) && !_context.Users.Any(r => r.Email == user2.Email) && !_context.Users.Any(r => r.Email == user3.Email))
            {
                var res = userManager.CreateAsync(user, "Qwerty-1").Result;
                var res2 = userManager.CreateAsync(user2, "Qwerty-1").Result;
                var res3 = userManager.CreateAsync(user3, "Qwerty-1").Result;
                res = userManager.AddToRoleAsync(user2, "User").Result;
                res = userManager.AddToRoleAsync(user3, "User").Result;
                res = userManager.AddToRoleAsync(user, "User").Result;
            }
        }
            

        public static void  SeedData (IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<DBContext>();
                //var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
                SeedUsers(manager,managerRole,context);
            }
        }

    }
}

