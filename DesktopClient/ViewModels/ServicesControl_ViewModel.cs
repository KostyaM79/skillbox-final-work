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
using System.Windows.Media;
using DesktopClient.General;
using System.IO;
using System.Windows;
using DesktopClient.Dialogs;
using System.Windows.Input;

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
        private RelayCommand editCmd;
        private RelayCommand deleteCmd;
        private ServicesControl parentWnd;

        private ServiceItem[] serviceItems;
        private bool viewMode = false;

        public ServicesControl_ViewModel(IDesktopServicesService service)
        {
            Icon icon = new Icon();
            editIcon = icon.GetPenIcon();
            deleteIcon = icon.GetBasketIcon();
            this.service = service;
            GetDataAsync();
        }

        public ServicesControl_ViewModel(IDesktopServicesService service, bool viewMode) : this(service)
        {
            this.viewMode = viewMode;
        }


        public ServicesControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                Update();
            }
        }

        private void Update()
        {
            parentWnd.itemsStack.Children.Clear();

            foreach (ServiceItem t in serviceItems)
            {
                if (viewMode)
                {
                    t.contentGrid.Children.Remove(t.deleteButton);
                    t.contentGrid.Children.Remove(t.editButton);
                }
                parentWnd.itemsStack.Children.Add(t);
            }
        }

        public ServiceModel[] Services
        {
            get => services;
            set
            {
                services = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Services)));
            }
        }



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
        private void GetDataAsync()
        {
            ServiceModel[] services = service.GetAllAsync().Result;
            Services = services;

            List<ServiceItem> items = new List<ServiceItem>();

            foreach (ServiceModel t in services)
            {
                ServiceItem item = new ServiceItem();
                item.DataContext = t;
                item.deleteButton.Command = new RelayCommand(DeleteAction);
                item.editButton.Command = new RelayCommand(EditAction);
                item.MouseLeftButtonDown += OnMouseClick;
                items.Add(item);
            }

            serviceItems = items.ToArray();
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
                        Update();
                    }
                }));
            }
        }

        public RelayCommand Edit_Cmd
        {
            get
            {
                return editCmd ?? (editCmd = new RelayCommand(obj =>
                {
                    ServiceDialog_ViewModel viewModel = new ServiceDialog_ViewModel(ServiceFactory.GetService<IDesktopServicesService>());
                    ServiceDialog dialog = new ServiceDialog(viewModel);
                    viewModel.Edit(SelectedService);
                    if (dialog.ShowDialog().Value)
                    {
                        GetDataAsync();
                    }
                }));
            }
        }

        public RelayCommand Delete_Cmd
        {
            get
            {
                return deleteCmd ?? (deleteCmd = new RelayCommand(obj =>
                {

                }));
            }
        }

        private void EditAction(object o)
        {
            ServiceDialog_ViewModel viewModel = new ServiceDialog_ViewModel(ServiceFactory.GetService<IDesktopServicesService>());
            ServiceDialog dialog = new ServiceDialog(viewModel);
            ServiceItem i = serviceItems.FirstOrDefault(e => (e.DataContext as ServiceModel).Id == (int)o);
            viewModel.Edit(i.DataContext as ServiceModel);
            if (dialog.ShowDialog().Value)
            {
                GetDataAsync();
                Update();
            }
        }

        private void DeleteAction(object o)
        {
            service.Delete((int)o);
            GetDataAsync();
            Update();
        }

        private ServiceItem selectedItem;

        private void OnMouseClick(object sender, MouseButtonEventArgs args)
        {
            ExpandItem(sender as ServiceItem);
        }

        private void ExpandItem(ServiceItem item)
        {
            if (selectedItem != null)
            {
                selectedItem.Height = double.NaN;
                selectedItem.elements.Children.Remove(selectedItem.elements.Children[1]);
            }

            selectedItem = item;
            TextBlock t = new TextBlock();
            selectedItem.elements.Children.Add(t);
            (selectedItem.elements.Children[1] as TextBlock).Text = (selectedItem.DataContext as ServiceModel).ServiceDescr;
            (selectedItem.elements.Children[1] as TextBlock).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            (selectedItem.elements.Children[1] as TextBlock).VerticalAlignment = VerticalAlignment.Stretch;
            (selectedItem.elements.Children[1] as TextBlock).Padding = new Thickness(5);
            (selectedItem.elements.Children[1] as TextBlock).Margin = new Thickness(5);
            (selectedItem.elements.Children[1] as TextBlock).Height = 100;
        }
    }
}
