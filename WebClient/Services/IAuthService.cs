using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IAuthService
    {
        string Authenticate(LoginModel model);
    }
}
