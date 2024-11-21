using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Models;

namespace DesktopClient.Services
{
    public interface IDesktopServicesService : IServicesService
    {
        Task<ServiceModel[]> GetAllAsync();
    }
}
