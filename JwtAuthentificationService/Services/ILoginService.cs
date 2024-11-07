using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JwtAuthenticationService.Models;

namespace JwtAuthenticationService.Services
{
    public interface ILoginService
    {
        Task<string> Login(LoginModel model);
    }
}
