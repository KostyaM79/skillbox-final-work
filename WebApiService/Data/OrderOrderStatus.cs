using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class OrderOrderStatus
    {
        public int Id { get; set; }

        public string OrderStatus { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
