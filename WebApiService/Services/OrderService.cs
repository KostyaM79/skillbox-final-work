using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using Models;
using Services;

namespace WebApiService.Services
{
    /// <summary>
    /// Управляет заявками
    /// </summary>
    public class OrderService : IApiOrderService
    {
        private AppDbContext context;

        public OrderService(AppDbContext context)
        {
            this.context = context;
        }

        public bool Add(OrderModel model)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@lastName", model.LastName);
                SqlParameter param2 = new SqlParameter("@firstName", model.FirstName);
                SqlParameter param3 = new SqlParameter("@email", model.Email);
                SqlParameter param4 = new SqlParameter("@orderText", model.Message);
                context.Database.ExecuteSqlRaw("AddOrderProc @lastName, @firstName, @email, @orderText", param1, param2, param3, param4);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public OrdersListModel GetAll()
        {
            Order[] orders = GetOrders();
            return CreateOrdersList(orders, context.Orders.Count());
        }

        public OrdersListModel Get(Func<Order, bool> predicate)
        {
            Order[] orders = GetOrders().Where(predicate).ToArray();
            return CreateOrdersList(orders, context.Orders.Count());
        }



        public ModifyOrderModel Get(int id)
        {
            Order order = context.Orders.AsNoTracking()
                .Include(c => c.Client).ThenInclude(c => c.LastName)
                .Include(c => c.Client).ThenInclude(c => c.FirstName)
                .Include(c => c.Client).ThenInclude(c => c.Email)
                .Include(c => c.OrderStatus)
                .FirstOrDefault(e => e.Id == id);

            return new ModifyOrderModel()
            {
                Id = order.Id,
                LastName = order.Client.LastName.LastName,
                FirstName = order.Client.FirstName.FirstName,
                ClientId = order.ClientId,
                CreatingDate = order.CreatingDate,
                Email = order.Client.Email.Email,
                Message = order.OrderText,
                OrderStatus = new OrderStatusModel() { Id = order.OrderStatus.Id, OrderStatus = order.OrderStatus.OrderStatus },
                Statuses = context.OrderStatuses.Select(e => new OrderStatusModel() { Id = e.Id, OrderStatus = e.OrderStatus }).ToArray()
            };
        }

        private Order[] GetOrders()
        {
            return context.Orders.AsNoTracking()
                .Include(c => c.Client).ThenInclude(c => c.LastName)
                .Include(c => c.Client).ThenInclude(c => c.FirstName)
                .Include(c => c.Client).ThenInclude(c => c.Email)
                .Include(c => c.OrderStatus)
                .ToArray();
        }

        private OrdersListModel CreateOrdersList(Order[] orders, int allOrdersCount)
        {
            OrderFullDataModel[] m = orders.Select(e => new OrderFullDataModel()
            {
                LastName = e.Client.LastName.LastName,
                FirstName = e.Client.FirstName.FirstName,
                Email = e.Client.Email.Email,
                ClientId = e.ClientId,
                CreatingDate = e.CreatingDate,
                Id = e.Id,
                OrderStatus = new() { Id = e.OrderStatus.Id, OrderStatus = e.OrderStatus.OrderStatus },
                Message = e.OrderText
            }).ToArray();

            return new OrdersListModel() { AllOrdersCount = allOrdersCount, OrdersList = m };
        }

        public void Update(UpdateOrderModel model)
        {
            Order order = context.Orders.FirstOrDefault(e => e.Id == model.Id);
            order.OrderStatusId = model.StatusId;
            context.SaveChanges();
        }
    }
}
