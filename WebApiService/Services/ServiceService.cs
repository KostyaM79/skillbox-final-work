using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using Services;
using Models;

namespace WebApiService.Services
{
    public class ServiceService : IServicesService
    {
        private readonly AppDbContext context;

        public ServiceService(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(ServiceModel model)
        {
            throw new NotImplementedException();
        }

        public ServiceModel[] GetAll()
        {
            Service[] services = context.Services.AsNoTracking().ToArray();

            List<ServiceModel> models = new();

            foreach (Service s in services)
            {
                models.Add(new ServiceModel()
                {
                    ServiceTitle = s.ServiceCaption,
                    ServiceDescr = s.ServiceDescription
                });
            }

            return models.ToArray();
        }
    }
}
