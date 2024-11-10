using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrdersListModel
    {
        public int AllOrdersCount { get; set; }

        public OrderFullDataModel[] OrdersList { get; set; }
    }
}
