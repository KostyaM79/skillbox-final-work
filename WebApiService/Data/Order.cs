using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime CreatingDate { get; set; }

        public string OrderText { get; set; }

        public int OrderStatusId { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
