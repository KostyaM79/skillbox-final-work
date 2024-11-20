using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using DesktopClient.UserControls;
using Models;
using DesktopClient.Events;

namespace DesktopClient.ViewModels
{
    public class OrderItemList_ViewModel : INotifyPropertyChanged
    {
        public event SelectedOrderChanged_EventHandler SelectedOrderChanged;

        private OrderFullDataModel selectedOrder;
        private OrderFullDataModel[] allOrders;
        private OrderFullDataModel[] orders;

        public OrderItemList Wnd { get; set; }

        public OrderFullDataModel[] OrderItems
        {
            get => orders;
            set
            {
                orders = value;
                if (allOrders == null) allOrders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrderItems)));
            }
        }

        public OrderFullDataModel SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                SelectedOrderChanged(new SelectedOrderChangedEventArgs() { Order = value });
            }
        }

        public void Filter(DateTime date)
        {
            OrderItems = allOrders.Where(e => e.CreatingDate.Date == date).ToArray();
        }

        public void Filter(DateTime date1, DateTime date2)
        {
            OrderItems = allOrders.Where(e => e.CreatingDate.Date >= date1 && e.CreatingDate.Date <= date2).ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
