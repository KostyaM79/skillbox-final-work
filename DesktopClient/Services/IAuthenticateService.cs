using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Services
{
    interface IAuthenticateService
    {
        string Login(LoginModel model);
    }
}
