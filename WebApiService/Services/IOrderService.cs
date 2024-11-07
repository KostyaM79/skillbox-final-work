using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebApiService.Services
{
    public interface IOrderService
    {
        bool Add(OrderModel model);

        OrderFullDataModel[] GetAll();

        ModifyOrderModel GetOrder(int id);

        bool Update(UpdateOrderModel model);
    }
}
