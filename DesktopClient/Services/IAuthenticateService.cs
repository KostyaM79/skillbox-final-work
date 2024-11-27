using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Services
{
    public interface IAuthenticateService
    {
        string Login(LoginModel model);

        Task<string> LoginAsync(LoginModel model);
    }
}
