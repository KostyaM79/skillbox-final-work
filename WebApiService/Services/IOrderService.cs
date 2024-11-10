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

        OrdersListModel GetAll();

        OrdersListModel Get(Func<Order, bool> predicate);

        OrdersListModel GetByDate(DateTime date);

        OrdersListModel GetByWeek();

        ModifyOrderModel GetOrder(int id);

        bool Update(UpdateOrderModel model);
    }
}
