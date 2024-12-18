﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.General;
using Models;

namespace DesktopClient.Services
{
    /// <summary>
    /// Управляет услугами
    /// </summary>
    public class ServicesService : IDesktopServicesService
    {
        private Server server = Server.Create();

        public void Add(ServiceModel model)
        {
            server.AddService(model);
        }

        public void Delete(int id)
        {
            server.DeleteService(id);
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
            server.UpdateService(model);
        }
    }
}
