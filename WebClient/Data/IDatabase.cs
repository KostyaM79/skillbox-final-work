using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebClient.Data
{
    public interface IDatabase
    {
        bool AddOrder(OrderModel model);

        OrderFullDataModel[] GetAllOrders();

        ModifyOrderModel GetOrder(int id);

        bool UpdateOrder(UpdateOrderModel model);
    }
}
