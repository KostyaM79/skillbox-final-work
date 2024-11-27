using DesktopClient.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.UserControls
{
    public interface ILoginFormViewModel
    {
        event AuthorizedEventHandler Authorized;
        event UnauthorizedEventHandler Unauthorized;

        AppStartUserControl Owner { get; set; }

        RelayCommand Login_Cmd { get; }

        bool FormIsEnabled { get; set; }
    }
}
