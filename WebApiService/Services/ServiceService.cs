using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using WebApiService.Models;

namespace WebApiService.Services
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext context;

        public ServiceService(AppDbContext context)
        {
            this.context = context;
        }

        public ServiceModel[] GetAll()
        {
            Service[] services = context.Services.AsNoTracking().ToArray();

            List<ServiceModel> models = new();

            foreach (Service s in services)
            {
                models.Add(new ServiceModel()
                {
                    ServiceCaption = s.ServiceCaption,
                    ServiceDescription = s.ServiceDescription
                });
            }

            return models.ToArray();
        }
    }
}
