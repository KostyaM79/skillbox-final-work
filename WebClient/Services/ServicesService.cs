using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using WebClient.Data;

namespace WebClient.Services
{
    /// <summary>
    /// Управляет услугами
    /// </summary>
    public class ServicesService : IServicesService
    {
        private readonly IDatabase database;

        public ServicesService(IDatabase database)
        {
            this.database = database;
        }

        public void Add(ServiceModel model, string token)
        {
            database.AddService(model, token);
        }

        public ServiceModel Get(int id)
        {
            return database.GetService(id);
        }

        public ServiceModel[] GetAll()
        {
            return database.GetAllServices();
        }

        public void Update(ServiceModel model, string token)
        {
            database.UpdateService(model, token);
        }

        public void Delete(int id, string token)
        {
            database.DeleteService(id, token);
        }
    }
}
