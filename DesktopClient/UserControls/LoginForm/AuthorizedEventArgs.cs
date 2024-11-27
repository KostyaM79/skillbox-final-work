using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.UserControls
{
    public class AuthorizedEventArgs : EventArgs
    {
        public AuthorizedEventArgs(string token)
        {
            Jwt = token;
        }

        public string Jwt { get; private set; }
    }
}
