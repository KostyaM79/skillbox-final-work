using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using WebClient.Data;

namespace WebClient.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IDatabase database;

        public ServicesService(IDatabase database)
        {
            this.database = database;
        }

        public void Add(ServiceModel model)
        {
            database.AddService(model);
        }

        public ServiceModel Get(int id)
        {
            return database.GetService(id);
        }

        public ServiceModel[] GetAll()
        {
            return database.GetAllServices();
        }

        public void Update(ServiceModel model)
        {
            database.UpdateService(model);
        }
    }
}
