using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IOrderService
    {
        bool Add(OrderModel model);

        OrdersListModel GetAll();

        ModifyOrderModel Get(int id);

        void Update(UpdateOrderModel model);
    }
}
