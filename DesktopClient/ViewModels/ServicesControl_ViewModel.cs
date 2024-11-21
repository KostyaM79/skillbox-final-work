using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DesktopClient.Services;
using DesktopClient.UserControls;
using Models;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DesktopClient.General;
using System.IO;
using System.Windows;
using DesktopClient.Dialogs;

namespace DesktopClient.ViewModels
{
    public class ServicesControl_ViewModel : INotifyPropertyChanged
    {
        private IDesktopServicesService service;
        private ServiceModel[] services;
        private ServiceModel selectedService;
        private Image editIcon;
        private Image deleteIcon;
        private RelayCommand createServiceCmd;

        public ServicesControl_ViewModel(IDesktopServicesService service)
        {
            Icon icon = new Icon();
            editIcon = icon.GetPenIcon();
            deleteIcon = icon.GetBasketIcon();
            this.service = service;
            GetDataAsync();
        }

        public ServicesControl ParentWnd { get; set; }

        public ServiceModel[] Services
        {
            get => services;
            set
            {
                services = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Services)));
            }
        }

        public Image Pen => editIcon;

        public Image Basket => deleteIcon;

        public ServiceModel SelectedService
        {
            get => selectedService;
            set
            {
                selectedService = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedService)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Загружает данные с сервера
        /// </summary>
        private async void GetDataAsync()
        {
            ServiceModel[] services = await service.GetAllAsync();
            Services = services;
        }

        public RelayCommand CreateService_Cmd
        {
            get
            {
                return createServiceCmd ?? (createServiceCmd = new RelayCommand(obj =>
                {
                    ServiceDialog_ViewModel viewModel = new ServiceDialog_ViewModel(ServiceFactory.GetService<IDesktopServicesService>());
                    ServiceDialog dialog = new ServiceDialog(viewModel);
                    if (dialog.ShowDialog().Value)
                    {
                        GetDataAsync();
                    }
                }));
            }
        }


        private void EditAction(object o)
        {
            
        }

        private void DeleteAction(object o)
        {
            
        }
    }
}
