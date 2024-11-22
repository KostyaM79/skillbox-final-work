using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using DesktopClient.UserControls;
using DesktopClient.General;
using DesktopClient.Services;
using DesktopClient.ViewModels;
using Services;
using Models;
using DesktopClient.Events;
using DesktopClient.Dialogs;

namespace DesktopClient.ViewModels
{
    public class Admin_ViewModel : INotifyPropertyChanged
    {
        private string token;
        private UserControl startControl = new AdminCanvas();
        private UserControl ordersFilterContron = new OrdersFilterControl();
        private UserControl content = new OrdersControl();
        private OrderItemList ordersItems;
        private OrderItemList_ViewModel orderItemListViewModel;
        private DateTime firstDate = DateTime.Today;
        private DateTime lastDate = DateTime.Today;

        private RelayCommand desktopCmd;
        private RelayCommand todayFilterCmd;
        private RelayCommand yesterdayFilterCmd;
        private RelayCommand weekFilterCmd;
        private RelayCommand monthFilterCmd;
        private RelayCommand rangeFilterCmd;
        private RelayCommand mainCmd;
        private RelayCommand projectsCmd;
        private RelayCommand servicesCmd;
        private RelayCommand blogCmd;
        private RelayCommand contactsCmd;

        public Admin_ViewModel(string token)
        {
            this.token = token;
            GetOrders();
        }

        public UserControl StartControl => startControl;

        public UserControl ContentControl
        {
            get => content;
            set
            {
                content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentControl)));
            }
        }

        public OrderItemList OrdersItems
        {
            get => ordersItems;
            set
            {
                ordersItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrdersItems)));
            }
        }

        public DateTime FirstDate
        {
            get => firstDate;
            set
            {
                firstDate = value;
                ApplyRangeFilter();
            }
        }

        public DateTime LastDate
        {
            get => lastDate;
            set
            {
                lastDate = value;
                ApplyRangeFilter();
            }
        }

        public UserControl OrdersFilter => ordersFilterContron;

        public RelayCommand Desktop_Cmd
        {
            get
            {
                return desktopCmd ?? (desktopCmd = new RelayCommand(obj =>
                {
                    GetOrders();
                }));
            }
        }

        public RelayCommand Main_Cmd
        {
            get
            {
                return mainCmd ?? (mainCmd = new RelayCommand(obj =>
                {
                    ContentControl = new MainContentControl();
                }));
            }
        }

        public RelayCommand Projects_Cmd
        {
            get
            {
                return projectsCmd ?? (projectsCmd = new RelayCommand(obj =>
                {
                    ProjectsControl_ViewModel viewModel = new ProjectsControl_ViewModel(ServiceFactory.GetService<IDesktopProjectsService>());
                    ContentControl = new ProjectsControl(viewModel);
                }));
            }
        }

        public RelayCommand Services_Cmd
        {
            get
            {
                return servicesCmd ?? (servicesCmd = new RelayCommand(obj =>
                {
                    ServicesControl_ViewModel viewModel = new ServicesControl_ViewModel(ServiceFactory.GetService<IDesktopServicesService>());
                    ContentControl = new ServicesControl(viewModel);
                }));
            }
        }

        public RelayCommand Blog_Cmd
        {
            get
            {
                return blogCmd ?? (blogCmd = new RelayCommand(obj =>
                {
                    Blog_ViewModel viewModel = new Blog_ViewModel(ServiceFactory.GetService<IDesktopArticlesService>());
                    ContentControl = new BlogControl(viewModel);
                }));
            }
        }

        public RelayCommand Contacts_Cmd
        {
            get
            {
                return contactsCmd ?? (contactsCmd = new RelayCommand(obj =>
                {
                    ContentControl = Contacts_VievModel.CreateContectsControl().ParentWnd;
                }));
            }
        }

        public RelayCommand TodayFilter_Cmd
        {
            get
            {
                return todayFilterCmd ?? (todayFilterCmd = new RelayCommand(obj =>
                {
                    if (orderItemListViewModel != null)
                        orderItemListViewModel.Filter(DateTime.Today);
                }));
            }
        }

        public RelayCommand YesterdayFilter_Cmd
        {
            get
            {
                return yesterdayFilterCmd ?? (yesterdayFilterCmd = new RelayCommand(obj =>
                {
                    if (orderItemListViewModel != null)
                        orderItemListViewModel.Filter(DateTime.Today.AddDays(-1));
                }));
            }
        }

        public RelayCommand WeekFilter_Cmd
        {
            get
            {
                return weekFilterCmd ?? (weekFilterCmd = new RelayCommand(obj =>
                {
                    if (orderItemListViewModel != null)
                        orderItemListViewModel.Filter(DateTime.Today.AddDays(-6), DateTime.Today);
                }));
            }
        }

        public RelayCommand MonthFilter_Cmd
        {
            get
            {
                return monthFilterCmd ?? (monthFilterCmd = new RelayCommand(obj =>
                {
                    if (orderItemListViewModel != null)
                        orderItemListViewModel.Filter(DateTime.Today.AddMonths(-1), DateTime.Today);
                }));
            }
        }

        public RelayCommand RangeFilter_Cmd
        {
            get
            {
                return rangeFilterCmd ?? (rangeFilterCmd = new RelayCommand(obj =>
                {
                    ApplyRangeFilter();
                }));
            }
        }

        private void ApplyRangeFilter()
        {
            if (orderItemListViewModel != null)
            {
                if (FirstDate != default && LastDate != default)
                    orderItemListViewModel.Filter(FirstDate, LastDate);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSelectedOrderChanged(SelectedOrderChangedEventArgs args)
        {
            EditOrderDialog_ViewModel viewModel = new EditOrderDialog_ViewModel(ServiceFactory.GetService<IOrderService>(), args.Order.Id);
            EditOrderDialog dialog = new EditOrderDialog(viewModel);
            dialog.ShowDialog();
        }

        private void GetOrders()
        {
            ContentControl = new OrdersControl();
            IOrderService service = ServiceFactory.GetService<IOrderService>();
            OrdersListModel model = service.GetAll();
            orderItemListViewModel = new OrderItemList_ViewModel();
            orderItemListViewModel.SelectedOrderChanged += OnSelectedOrderChanged;
            OrderItemList itemList = new OrderItemList(orderItemListViewModel);
            orderItemListViewModel.OrderItems = model.OrdersList;
            OrdersItems = itemList;
        }
    }
}
