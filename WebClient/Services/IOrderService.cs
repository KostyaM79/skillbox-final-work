using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebClient.Services
{
    public interface IOrderService
    {
        bool Add(OrderModel model);

        OrderFullDataModel[] GetAll();

        OrderFullDataModel Get(int id);

        bool UpdateOrder(UpdateOrderModel model);
    }
}
