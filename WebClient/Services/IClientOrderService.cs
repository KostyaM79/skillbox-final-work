using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Models;

namespace WebClient.Services
{
    public interface IClientOrderService : IOrderService
    {
        OrdersListModel GetByToday();

        OrdersListModel GetByYesterday();

        OrdersListModel GetByWeek();

        OrdersListModel Get(string filterName, int startOffset, int endOffset);
    }
}
