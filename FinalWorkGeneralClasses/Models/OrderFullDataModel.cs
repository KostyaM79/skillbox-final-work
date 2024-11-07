using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class OrderFullDataModel : OrderModel
    {
        public int Id { get; set; }

        public OrderStatusModel OrderStatus { get; set; }

        public DateTime CreatingDate { get; set; }

        public int ClientId { get; set; }
    }
}
