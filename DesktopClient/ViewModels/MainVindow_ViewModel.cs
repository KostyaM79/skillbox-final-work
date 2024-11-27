using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using DesktopClient.UserControls;
using DesktopClient.General;
using DesktopClient.Services;
using System.Windows;

namespace DesktopClient.ViewModels
{
    public class MainVindow_ViewModel
    {
        private readonly MainWindow wnd;
        private ILoginFormViewModel loginFormViewModel = LoginForm_ViewModel.Create(ServiceFactory.GetService<IAuthenticateService>());
        private readonly UserControl startControl;
        private RelayCommand continueAsGuestCmd;

        public MainVindow_ViewModel(MainWindow window)
        {
            startControl = loginFormViewModel.Owner;
            loginFormViewModel.Authorized += OnAuthorized;
            loginFormViewModel.Unauthorized += OnUnauthorized;
            wnd = window;
        }

        /// <summary>
        /// Отображает форму входа в главном окне
        /// </summary>
        public UserControl StartControl
        {
            get => startControl;
        }

        /// <summary>
        /// Выполняется, если пользователь нажал "Продолжить как гость".
        /// Отображает главное окно для работы с гостями.
        /// </summary>
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

        private void OnAuthorized(object sender, AuthorizedEventArgs args)
        {
            wnd.DataContext = new Admin_ViewModel(args.Jwt);
        }

        private void OnUnauthorized(object sender, UnauthorizedEventArgs args)
        {
            MessageBox.Show(wnd, Constants.UNAUTHORIZE_ERROR_MESSAGE, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
