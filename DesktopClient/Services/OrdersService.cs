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
    public class OrdersService : IOrderService
    {
        private readonly Server server = new Server();

        public bool Add(OrderModel model)
        {
            throw new NotImplementedException();
        }

        public ModifyOrderModel Get(int id)
        {
            return server.GetOrder(id);
        }

        public OrdersListModel GetAll()
        {
            return server.GetAllOrders();
        }

        public void Update(UpdateOrderModel model)
        {
            server.UpdateOrder(model);
        }
    }
}
