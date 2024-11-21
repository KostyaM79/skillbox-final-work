using DesktopClient.ViewModels;
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

namespace DesktopClient.UserControls
{
    /// <summary>
    /// Логика взаимодействия для GuestMainControl.xaml
    /// </summary>
    public partial class GuestMainControl : UserControl
    {
        public GuestMainControl(GuestMain_ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ParentWnd = this;
        }
    }
}
