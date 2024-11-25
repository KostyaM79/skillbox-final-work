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
        /// <summary>
        /// Вызывается при изменении значения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
            Server.SetToken(token);
            GetOrders();
        }

        public UserControl StartControl => startControl;

        /// <summary>
        /// Возвращает или задаёт элемент управления, содержащий главный контент окна
        /// </summary>
        public UserControl ContentControl
        {
            get => content;
            set
            {
                content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentControl)));
            }
        }

        /// <summary>
        /// Возвращает или задаё элемент управления, отображающий список заявок
        /// </summary>
        public OrderItemList OrdersItems
        {
            get => ordersItems;
            set
            {
                ordersItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrdersItems)));
            }
        }

        /// <summary>
        /// Возвращает или задаёт начальную дату для фильтрации заявок
        /// </summary>
        public DateTime FirstDate
        {
            get => firstDate;
            set
            {
                firstDate = value;
                ApplyRangeFilter();
            }
        }

        /// <summary>
        /// Возвращает или задаёт конечную дату для фильтрации заявок
        /// </summary>
        public DateTime LastDate
        {
            get => lastDate;
            set
            {
                lastDate = value;
                ApplyRangeFilter();
            }
        }

        /// <summary>
        /// Возвращает панель филтра
        /// </summary>
        public UserControl OrdersFilter => ordersFilterContron;

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает рабочий стол с заявками
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает главное окно.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает проекты.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает услуги.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает блог.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке меню.
        /// Отображает контакты.
        /// </summary>
        public RelayCommand Contacts_Cmd
        {
            get
            {
                return contactsCmd ?? (contactsCmd = new RelayCommand(obj =>
                {
                    ContentControl = ContentControl = Contacts_VievModel.CreateContectsControl(ServiceFactory.GetService<IDesktopSocialsService>()).ParentWnd;
                }));
            }
        }

        /// <summary>
        /// Привязывается к кнопке фильтра.
        /// Отбирает заявки только текущей даты.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке фильтра.
        /// Отбирает заявки только предыдущей даты.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке фильтра.
        /// Отбирает заявки за неделю.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке фильтра.
        /// Отбирает заявки за месяц.
        /// </summary>
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

        /// <summary>
        /// Привязывается к кнопке фильтра с датами.
        /// Отбирает заявки за указанный диапазон дат.
        /// </summary>
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

        /// <summary>
        /// Проверяет, выбраны ли обе даты. И, если так, то применяет фильтр.
        /// </summary>
        private void ApplyRangeFilter()
        {
            if (orderItemListViewModel != null)
            {
                if (FirstDate != default && LastDate != default)
                    orderItemListViewModel.Filter(FirstDate, LastDate);
            }
        }

        /// <summary>
        /// В ответ на изменение выбранной заявки открывает диалог для её редактирования.
        /// </summary>
        /// <param name="args"></param>
        private void OnSelectedOrderChanged(SelectedOrderChangedEventArgs args)
        {
            if (args.Order != null)
            {
                EditOrderDialog_ViewModel viewModel = new EditOrderDialog_ViewModel(ServiceFactory.GetService<IDesktopOrdersService>(), args.Order.Id, token);
                EditOrderDialog dialog = new EditOrderDialog(viewModel);
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Получает с сервера список заявок
        /// </summary>
        private void GetOrders()
        {
            ContentControl = new OrdersControl();
            IOrderService service = ServiceFactory.GetService<IDesktopOrdersService>();
            OrdersListModel model = service.GetAll();
            orderItemListViewModel = new OrderItemList_ViewModel();
            orderItemListViewModel.SelectedOrderChanged += OnSelectedOrderChanged;
            OrderItemList itemList = new OrderItemList(orderItemListViewModel);
            orderItemListViewModel.OrderItems = model.OrdersList;
            OrdersItems = itemList;
        }
    }
}
