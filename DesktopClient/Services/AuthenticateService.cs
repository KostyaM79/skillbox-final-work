using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.General;
using Models;

namespace DesktopClient.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly Server server = Server.Create();

        public string Login(LoginModel model)
        {
            return server.Login(model);
        }
    }
}
