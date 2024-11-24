using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class ServicesReceivedEventArgs
    {
        public ServicesReceivedEventArgs(ServiceModel[] services)
        {
            Services = services;
        }

        public ServiceModel[] Services
        {
            get;
            private set;
        }
    }
}
