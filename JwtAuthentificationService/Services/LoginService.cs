using Microsoft.AspNetCore.Identity;
using JwtAuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using UsersDatabase;

namespace JwtAuthenticationService.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> userManager;
        private readonly IJwtTokenService tokenService;

        public LoginService(UserManager<User> userManager, IJwtTokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Аутентифицирует пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns>В случае успешной аутентификации возвращает JWT-токен, в противном случае - null</returns>
        public async Task<string> Login(LoginModel model)
        {
            User user = await userManager.FindByNameAsync(model.Username);
            
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                IList<Claim> claims = await userManager.GetClaimsAsync(user);
                return tokenService.CreateToken(user, claims);
            }

            else return null;
        }
    }
}
