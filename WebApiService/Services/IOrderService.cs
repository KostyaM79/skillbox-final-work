using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebApiService.Data;

namespace WebApiService.Services
{
    public interface IOrderService
    {
        bool Add(OrderModel model);

        OrderFullDataModel[] GetAll();

        OrderFullDataModel[] Get(Func<Order, bool> predicate);

        OrderFullDataModel[] GetByDate(DateTime date);

        OrderFullDataModel[] GetByWeek();

        ModifyOrderModel GetOrder(int id);

        bool Update(UpdateOrderModel model);
    }
}
