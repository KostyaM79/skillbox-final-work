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

        public OrderFullDataModel[] GetAll()
        {
            return database.GetAllOrders();
        }

        public bool UpdateOrder(UpdateOrderModel model)
        {
            return database.UpdateOrder(model);
        }
    }
}
