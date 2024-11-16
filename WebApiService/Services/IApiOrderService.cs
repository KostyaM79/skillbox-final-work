using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Models;
using WebApiService.Data;

namespace WebApiService.Services
{
    public interface IApiOrderService : IOrderService
    {
        OrdersListModel Get(Func<Order, bool> predicate);
    }
}
