﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Models;

namespace WebApiService.Services
{
    public interface IOrderService
    {
        bool Add(OrderModel model);

        OrderFullDataModel[] GetAll();
    }
}
