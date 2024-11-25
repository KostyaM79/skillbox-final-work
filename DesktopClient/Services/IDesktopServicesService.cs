using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Services
{
    public interface IDesktopServicesService
    {
        ServiceModel[] GetAll();

        ServiceModel Get(int id);

        void Add(ServiceModel model);

        void Update(ServiceModel model);

        void Delete(int id);

        Task<ServiceModel[]> GetAllAsync();
    }
}
