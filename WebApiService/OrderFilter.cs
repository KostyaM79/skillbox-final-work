using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;

namespace WebApiService
{
    /// <summary>
    /// Содержит именованные фильтры для заявок
    /// </summary>
    public class OrderFilter
    {
        private readonly Dictionary<string, Func<Order, bool>> filters;

        private readonly int startOffset;
        private readonly int endOffset;
        private readonly string filterName;

        public OrderFilter(string filterName, int startOffset, int endOffset)
        {
            this.filterName = filterName;
            this.startOffset = startOffset;
            this.endOffset = endOffset;

            filters = new Dictionary<string, Func<Order, bool>>()
            {
                {"Today", (Order e) => e.CreatingDate.Date == DateTime.Today },
                {"Yesterday", (Order e) => e.CreatingDate.Date == DateTime.Today.AddDays(-1) },
                { "Week", (Order e) => e.CreatingDate.Date >= DateTime.Today.AddDays(-6) && e.CreatingDate.Date <= DateTime.Today },
                { "Month", (Order e) => e.CreatingDate.Date > DateTime.Today.AddMonths(-1) && e.CreatingDate.Date <= DateTime.Today },
                { "Range", (Order e) => e.CreatingDate.Date >= DateTime.Today.AddDays(-startOffset) && e.CreatingDate.Date <= DateTime.Today.AddDays(-endOffset) }
            };
        }

        public Func<Order, bool> Filter => filters[filterName];
    }
}
