using DesktopClient.General;
using DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Models;
using DesktopClient.ViewModels;

namespace DesktopClient.UserControls
{
    public class LoginForm_ViewModel : ILoginFormViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event AuthorizedEventHandler Authorized;
        public event UnauthorizedEventHandler Unauthorized;

        private IAuthenticateService service;
        private bool formIsEnabled = true;
        private RelayCommand loginCmd;

        public static ILoginFormViewModel Create(IAuthenticateService service)
        {
            ILoginFormViewModel viewModel = new LoginForm_ViewModel(service);
            AppStartUserControl control = new AppStartUserControl(viewModel);
            viewModel.Owner = control;
            return viewModel;
        }

        public LoginForm_ViewModel(IAuthenticateService service)
        {
            this.service = service;
        }

        public AppStartUserControl Owner { get; set; }

        /// <summary>
        /// Определяет состояние формы (активна/неактивна)
        /// </summary>
        public bool FormIsEnabled
        {
            get => formIsEnabled;
            set
            {
                formIsEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FormIsEnabled)));
            }
        }

        /// <summary>
        /// Привязывается к полю Имя в форме
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Отправляет на сервер данные для входа и получает JWT-токен, если аутентификация прошла успешно.
        /// </summary>
        public RelayCommand Login_Cmd
        {
            get
            {
                return loginCmd ?? (loginCmd = new RelayCommand(ExecuteLoginCmd));
            }
        }


        /// <summary>
        /// Выполняет команду входа асинхронно
        /// </summary>
        /// <param name="o"></param>
        private async void ExecuteLoginCmd(object o)
        {
            LockLoginForm();    // Блокируем форму входа, чтобы исключить её использоване, пока осуществляется запрос к серверу аутенитификации

            // Получаем сервис для аутентификации
            IAuthenticateService service = ServiceFactory.GetService<IAuthenticateService>();

            // Отправляем данные на сервер и получаем ответ
            string token = await service.LoginAsync(new LoginModel() { Username = Username, Password = Owner.passwordBox.Password });

            // Если токен получен, то отображаем рабочий стол администратора
            if (!string.IsNullOrEmpty(token))
                Authorized?.Invoke(this, new AuthorizedEventArgs(token));
            else
                Unauthorized?.Invoke(this, new UnauthorizedEventArgs());

            UnlockLoginForm();  // Разблокируем форму входа
        }

        /// <summary>
        /// Блокирует форму
        /// </summary>
        private void LockLoginForm()
        {
            FormIsEnabled = false;
        }

        /// <summary>
        /// Разблокирует форму
        /// </summary>
        private void UnlockLoginForm()
        {
            FormIsEnabled = true;
        }
    }
}
