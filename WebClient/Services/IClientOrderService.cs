using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebClient.Services
{
    public interface IClientOrderService
    {
        bool Add(OrderModel model);

        OrdersListModel GetAll();

        ModifyOrderModel Get(int id);

        OrdersListModel GetByToday();

        OrdersListModel GetByYesterday();

        OrdersListModel GetByWeek();

        OrdersListModel Get(string filterName, int startOffset, int endOffset);

        void Update(UpdateOrderModel model);
    }
}
