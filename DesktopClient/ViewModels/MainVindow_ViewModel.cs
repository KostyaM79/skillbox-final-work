using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DesktopClient.UserControls;
using DesktopClient.General;
using DesktopClient.Services;
using Models;

namespace DesktopClient.ViewModels
{
    public class MainVindow_ViewModel
    {
        private readonly MainWindow wnd;
        private readonly UserControl startControl = new AppStartUserControl();
        private RelayCommand continueAsGuestCmd;
        private RelayCommand loginCmd;

        public MainVindow_ViewModel(MainWindow window)
        {
            wnd = window;
        }

        public UserControl StartControl
        {
            get => startControl;
        }

        public string Username { get; set; }

        public RelayCommand ContinueAsGuest_Cmd
        {
            get
            {
                return continueAsGuestCmd ?? (continueAsGuestCmd = new RelayCommand(obj =>
                {
                    wnd.DataContext = new Guest_ViewModel();
                }));
            }
        }

        public RelayCommand Login_Cmd
        {
            get
            {
                return loginCmd ?? (loginCmd = new RelayCommand(obj =>
                {
                    IAuthenticateService service = ServiceFactory.GetService<IAuthenticateService>();
                    string token = service.Login(new LoginModel() { Username = Username, Password = (obj as PasswordBox).Password });
                    if (!string.IsNullOrEmpty(token)) wnd.DataContext = new Admin_ViewModel(token);
                }));
            }
        }
    }
}
