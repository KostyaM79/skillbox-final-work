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

        OrdersListModel GetAll();

        OrdersListModel GetByToday();

        OrdersListModel GetByYesterday();

        OrdersListModel GetByWeek();

        ModifyOrderModel Get(int id);

        OrdersListModel Get(string filterName, int startOffset = 0, int endOffset = 0);

        void UpdateOrder(UpdateOrderModel model);
    }
}
