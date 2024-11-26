using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using DesktopClient.General;

namespace DesktopClient.Services
{
    /// <summary>
    /// Управляет заявками
    /// </summary>
    public class OrdersService : IDesktopOrdersService
    {
        private readonly Server server = Server.Create();

        public bool Add(OrderModel model)
        {
            return server.AddOrder(model);
        }

        public ModifyOrderModel Get(int id)
        {
            return server.GetOrder(id);
        }

        public OrdersListModel GetAll()
        {
            return server.GetAllOrders();
        }

        public void Update(UpdateOrderModel model, string token)
        {
            server.UpdateOrder(model, token);
        }
    }
}
