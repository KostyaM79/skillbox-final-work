using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DesktopClient.Services;
using Services;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceFactory.Add<IAuthenticateService, AuthenticateService>();
            ServiceFactory.Add<IOrderService, OrdersService>();
            ServiceFactory.Add<IDesktopProjectsService, ProjectsService>();
            ServiceFactory.Add<IDesktopServicesService, ServicesService>();
        }
    }
}
