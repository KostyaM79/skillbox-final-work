using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using Models;

namespace WebApiService.Services
{
    public class OrderService : IOrderService
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

        public OrderFullDataModel[] GetAll()
        {
            Order[] orders = GetOrders();
            return CreateOrderModelsArray(orders);
        }

        public ModifyOrderModel GetOrder(int id)
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

        private OrderFullDataModel[] CreateOrderModelsArray(Order[] orders)
        {
            List<OrderFullDataModel> data = new();
            foreach (Order o in orders)
            {
                data.Add(new OrderFullDataModel()
                {
                    LastName = o.Client.LastName.LastName,
                    FirstName = o.Client.FirstName.FirstName,
                    Email = o.Client.Email.Email,
                    ClientId = o.ClientId,
                    CreatingDate = o.CreatingDate,
                    Id = o.Id,
                    OrderStatus = new() { Id = o.OrderStatus.Id, OrderStatus = o.OrderStatus.OrderStatus },
                    Message = o.OrderText
                });
            }

            return data.ToArray();
        }

        public bool Update(UpdateOrderModel model)
        {
            Order order = context.Orders.FirstOrDefault(e => e.Id == model.Id);
            order.OrderStatusId = model.StatusId;
            return context.SaveChanges() > 0;
        }
    }
}
