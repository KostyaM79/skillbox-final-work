using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Dialogs;
using DesktopClient.Services;
using DesktopClient.General;
using System.ComponentModel;
using Services;
using Models;

namespace DesktopClient.ViewModels
{
    public class EditOrderDialog_ViewModel : INotifyPropertyChanged
    {
        private string token;
        private IDesktopOrdersService service;
        private ModifyOrderModel order;
        private OrderStatusModel status;

        private RelayCommand okCmd;

        public EditOrderDialog_ViewModel(IDesktopOrdersService service, int id, string token)
        {
            this.token = token;
            this.service = service;
            order = service.Get(id);
            SelectedOrderStatus = Statuses.FirstOrDefault(e => e.Id == order.OrderStatus.Id);
        }

        public int Id => order.Id;

        public DateTime CreatingDate => order.CreatingDate;

        public string FirstName => order.FirstName;

        public string LastName => order.LastName;

        public string Message => order.Message;

        public string Email => order.Email;

        public OrderStatusModel[] Statuses => order.Statuses;

        public OrderStatusModel SelectedOrderStatus
        {
            get => status;
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedOrderStatus)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand Ok_Cmd
        {
            get
            {
                return okCmd ?? (okCmd = new RelayCommand(obj =>
                {
                    if (!SelectedOrderStatus.Equals(order.OrderStatus))
                        service.Update(new UpdateOrderModel() { Id = Id, StatusId = SelectedOrderStatus.Id }, token);
                    (obj as EditOrderDialog).Close();
                }));
            }
        }
    }
}
