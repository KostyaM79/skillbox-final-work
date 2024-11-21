using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.General;
using Models;

namespace DesktopClient.Services
{
    public class ServicesService : IDesktopServicesService
    {
        private Server server = new Server();

        public void Add(ServiceModel model)
        {
            server.AddService(model);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceModel[] GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceModel[]> GetAllAsync()
        {
            return await server.GetAllServicesAsync();
        }

        public void Update(ServiceModel model)
        {
            throw new NotImplementedException();
        }
    }
}
