using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Data;
using Models;

namespace WebClient.Services
{
    /// <summary>
    /// Управляет заявками
    /// </summary>
    public class OrderService : IClientOrderService
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

        public ModifyOrderModel Get(int id)
        {
            return database.GetOrder(id);
        }

        public OrdersListModel Get(string filterName, int startOffset, int endOffset)
        {
            return database.GetOrders(filterName, startOffset, endOffset);
        }

        public OrdersListModel GetAll()
        {
            return database.GetAllOrders();
        }

        //public OrdersListModel GetByToday()
        //{
        //    return database.GetOrders("Today", 0, 0);
        //}

        //public OrdersListModel GetByYesterday()
        //{
        //    return database.GetOrdersByYesterday();
        //}

        //public OrdersListModel GetByWeek()
        //{
        //    return database.GetOrdersByWeek();
        //}


        public void Update(UpdateOrderModel model, string token)
        {
            database.UpdateOrder(model, token);
        }
    }
}
