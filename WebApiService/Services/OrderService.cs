﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using WebApiService.Models;

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
                SqlParameter param4 = new SqlParameter("@orderText", model.OrderText);
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
            Order[] orders = context.Orders.AsNoTracking()
                .Include(c => c.Client).ThenInclude(c => c.LastName)
                .Include(c => c.Client).ThenInclude(c => c.FirstName)
                .Include(c => c.Client).ThenInclude(c => c.Email)
                .Include(c => c.OrderStatus)
                .ToArray();

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
                    OrderStatus = o.OrderStatus,
                    OrderText = o.OrderText
                });
            }

            return data.ToArray();
        }
    }
}
