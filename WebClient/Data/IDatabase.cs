using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace WebClient.Data
{
    public interface IDatabase
    {
        bool AddOrder(OrderModel model);

        bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName);

        OrdersListModel GetAllOrders();

        OrdersListModel GetOrders(string filterName, int startOffset, int endOffset);

        OrdersListModel GetOrdersByToday();

        OrdersListModel GetOrdersByYesterday();

        OrdersListModel GetOrdersByWeek();

        ModifyOrderModel GetOrder(int id);

        bool UpdateOrder(UpdateOrderModel model);
    }
}
