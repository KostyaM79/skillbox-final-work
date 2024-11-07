using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace JwtAuthenticationService.Services
{
    /// <summary>
    /// Сервис JWT-токенов
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Генерирует JWT-токен
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CreateToken(IdentityUser user, IList<Claim> claims)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            List<Claim> cl = new List<Claim>();
            cl.Add(new Claim("Id", user.UserName));
            cl.AddRange(claims);
            cl.Add(new Claim("Admin", "true"));

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(cl.ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
            };
            string token = handler.WriteToken(handler.CreateToken(descriptor));
            return token;
        }
    }
}
