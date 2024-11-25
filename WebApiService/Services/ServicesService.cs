using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using WebApiService.Services;
using Models;

namespace WebApiService.Services
{
    public class ServicesService : IServicesService
    {
        private readonly AppDbContext context;

        public ServicesService(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(ServiceModel model)
        {
            Service service = new Service() { ServiceCaption = model.ServiceTitle, ServiceDescription = model.ServiceDescr };
            context.Services.Add(service);
            context.SaveChanges();
        }

        public ServiceModel Get(int id)
        {
            Service service = context.Services.FirstOrDefault(e => e.Id == id);
            return new ServiceModel() { Id = service.Id, ServiceTitle = service.ServiceCaption, ServiceDescr = service.ServiceDescription };
        }

        public ServiceModel[] GetAll()
        {
            Service[] services = context.Services.AsNoTracking().ToArray();

            List<ServiceModel> models = new();

            foreach (Service s in services)
            {
                models.Add(new ServiceModel()
                {
                    Id = s.Id,
                    ServiceTitle = s.ServiceCaption,
                    ServiceDescr = s.ServiceDescription
                });
            }

            return models.ToArray();
        }

        public void Update(ServiceModel model)
        {
            Service service = context.Services.FirstOrDefault(e => e.Id == model.Id);
            service.ServiceCaption = model.ServiceTitle;
            service.ServiceDescription = model.ServiceDescr;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Service service = context.Services.FirstOrDefault(e => e.Id == id);
            context.Remove(service);
            context.SaveChanges();
        }
    }
}
