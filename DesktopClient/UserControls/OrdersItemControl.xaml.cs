using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;

namespace DesktopClient.UserControls
{
    /// <summary>
    /// Логика взаимодействия для OrdersItemControl.xaml
    /// </summary>
    public partial class OrdersItemControl : UserControl
    {
        public OrdersItemControl(OrderFullDataModel model)
        {
            InitializeComponent();
            SetData(model);
        }

        private void SetData(OrderFullDataModel model)
        {
            id.Text = $"{model.Id}";
            date.Text = model.CreatingDate.ToString("dd.MM.yyyy hh:mm");
            firstName.Text = model.FirstName;
            lastName.Text = model.LastName;
            orderText.Text = model.Message;
            mail.Text = model.Email;
        }
    }
}
