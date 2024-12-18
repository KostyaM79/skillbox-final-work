﻿using System;
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

        void Add(ServiceModel model, string token);

        void Update(ServiceModel model, string token);

        void Delete(int id, string token);
    }
}
