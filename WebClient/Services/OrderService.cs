using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Data;
using Models;

namespace WebClient.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDatabase database;

        public OrderService(IDatabase database)
        {
            this.database = database;
        }

        public bool Add(OrderModel model)
        {
            return database.AddOrder(model);
        }

        public OrderFullDataModel Get(int id)
        {
            return database.GetOrder(id);
        }

        public OrdersListModel Get(string filterName, int startOffset = 0, int endOffset = 0)
        {
            return database.GetOrders(filterName, startOffset, endOffset);
        }

        public OrdersListModel GetAll()
        {
            return database.GetAllOrders();
        }

        public OrdersListModel GetByToday()
        {
            return database.GetOrders("Today", 0, 0);
        }

        public OrdersListModel GetByYesterday()
        {
            return database.GetOrdersByYesterday();
        }

        public OrdersListModel GetByWeek()
        {
            return database.GetOrdersByWeek();
        }


        public bool UpdateOrder(UpdateOrderModel model)
        {
            return database.UpdateOrder(model);
        }
    }
}
