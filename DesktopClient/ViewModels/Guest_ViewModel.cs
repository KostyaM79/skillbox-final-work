using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Services;
using System.ComponentModel;
using System.Windows.Controls;
using DesktopClient.UserControls;
using DesktopClient.General;
using Services;

namespace DesktopClient.ViewModels
{
    public class Guest_ViewModel : INotifyPropertyChanged
    {
        private readonly UserControl startControl = new GuestCanvas();
        private UserControl content;

        private RelayCommand mainCmd;
        private RelayCommand projectsCmd;

        public Guest_ViewModel()
        {
            GuestMain_ViewModel viewModel = new GuestMain_ViewModel(ServiceFactory.GetService<IOrderService>());
            content = new GuestMainControl(viewModel);
        }

        public UserControl StartControl => startControl;

        public UserControl ContentControl
        {
            get => content;
            set
            {
                content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentControl)));
            }
        }




        public RelayCommand Main_Cmd
        {
            get
            {
                return mainCmd ?? (mainCmd = new RelayCommand(obj =>
                {
                    GuestMain_ViewModel viewModel = new GuestMain_ViewModel(ServiceFactory.GetService<IOrderService>());
                    ContentControl = new GuestMainControl(viewModel);
                }));
            }
        }

        public RelayCommand Projects_Cmd
        {
            get
            {
                return projectsCmd ?? (projectsCmd = new RelayCommand(obj =>
                {
                    GuestProjects_ViewModel viewModel = new GuestProjects_ViewModel(ServiceFactory.GetService<IDesktopProjectsService>());
                    ContentControl = new GuestProjectsControl(viewModel);
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
