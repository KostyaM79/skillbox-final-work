using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtAuthenticationService.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(IdentityUser user, IList<Claim> claims);
    }
}
