using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IServicesService
    {
        ServiceModel[] GetAll();

        ServiceModel Get(int id);

        void Add(ServiceModel model);

        void Update(ServiceModel model);
    }
}
