using JwtAuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using UsersDatabase;
using NotebookDatabase.Models;

namespace JwtAuthenticationService.Services
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> userManager;
        private readonly IJwtTokenService jwtTokenService;
        private readonly UsersDbContext context;

        public RegisterService(UserManager<User> userManager, IJwtTokenService jwtTokenService, UsersDbContext context)
        {
            this.userManager = userManager;
            this.jwtTokenService = jwtTokenService;
            this.context = context;
        }

        /// <summary>
        /// Регистрирует нового пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns>При успешной регистрации Jwt-токен, в противном случае - null</returns>
        public async Task<string> Register(RegisterModel model)
        {
            User user = new User() { UserName = model.Username };
            IdentityResult createResult = await userManager.CreateAsync(user, model.Password);

            if (createResult.Succeeded)
            {
                Claim userClaim = new Claim("IsUser", "");
                IdentityResult res = await userManager.AddClaimAsync(user, userClaim);

                if (res.Succeeded)
                    return jwtTokenService.CreateToken(user, await userManager.GetClaimsAsync(user));
            }

            return null;
        }

        public async Task<UserModel> RegisterAdmin(RegisterModel model)
        {
            User user = new User() { UserName = model.Username };
            IdentityResult createResult = await userManager.CreateAsync(user, model.Password);

            if (createResult.Succeeded)
            {
                Claim userClaim = new Claim("IsAdmin", "");
                IdentityResult res = await userManager.AddClaimAsync(user, userClaim);

                if (res.Succeeded)
                {
                    User u = await userManager.FindByNameAsync(model.Username);
                    string[] claims = (await userManager.GetClaimsAsync(u)).Select(e => e.Type).ToArray();
                    return new UserModel() { Id = u.Id, Username = u.UserName, Claims = claims };
                }
            }

            return null;
        }

        public UserModel[] GetUsers()
        {
            User[] users = context.Users.ToArray();
            UserModel[] models = users.Select(e => new UserModel() { Id = e.Id, Username = e.UserName }).ToArray();

            foreach (UserModel u in models)
            {
                IdentityUserClaim<string>[] claims = context.UserClaims.Where(e => e.UserId == u.Id).ToArray();
                u.Claims = claims.Select(e => e.ClaimType).ToArray();
            }

            return models;
        }

        public void Delete(string id)
        {
            User u = userManager.FindByIdAsync(id).Result;
            userManager.DeleteAsync(u);
        }
    }
}
