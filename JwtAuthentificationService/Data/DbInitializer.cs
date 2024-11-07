using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersDatabase;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationService.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Если в базе данных нет ни одного пользователя, то добавляем администратора
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="context"></param>
        public static void Initialize(UserManager<User> userManager, UsersDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;

            User user = new User() { UserName = "Admin" };
            IdentityResult result = userManager.CreateAsync(user, "1_Qwerty").Result;

            Claim claim = new Claim("IsAdmin", "");
            IdentityResult result2 = userManager.AddClaimAsync(user, claim).Result;
        }
    }
}
