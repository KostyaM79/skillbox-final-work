using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Services;
using DesktopClient.UserControls;
using DesktopClient.Events;
using Models;

namespace DesktopClient.ViewModels
{
    public class GuestServices_ViewModel
    {
        private GuestServicesControl parentWnd;
        private ServiceModel[] services;

        /// <summary>
        /// Вызывается при необходимости обновить данные
        /// </summary>
        public event RequestingServicesEventHandler RequestingData;

        /// <summary>
        /// Вызывается, когда получены новые данные
        /// </summary>
        public event ServicesReceivedEventHandler DataReceived;

        private IDesktopServicesService service;

        public static GuestServices_ViewModel CreateControl(IDesktopServicesService service)
        {
            GuestServices_ViewModel viewModel = new GuestServices_ViewModel(service);
            GuestServicesControl control = new GuestServicesControl(viewModel);
            viewModel.ParentWnd = control;

            return viewModel;
        }

        public GuestServices_ViewModel(IDesktopServicesService service)
        {
            this.service = service;
        }

        public ServiceModel[] Services
        {
            get => services;
            set
            {
                services = value;
                DataReceived?.Invoke(this, new ServicesReceivedEventArgs(services));
            }
        }

        public GuestServicesControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                RequestingData?.Invoke(parentWnd, new RequestingServicesEventArgs());
            }
        }

        private void OnRequestionData(object sender, RequestingServicesEventArgs args)
        {
            Services = service.GetAll();
        }

        private void OnDataReceived(object sender, ServicesReceivedEventArgs args)
        {
            
        }
    }
}