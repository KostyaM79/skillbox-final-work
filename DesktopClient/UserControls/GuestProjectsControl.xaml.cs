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
using Services;
using DesktopClient.ViewModels;

namespace DesktopClient.UserControls
{
    /// <summary>
    /// Логика взаимодействия для GuestProjectsControl.xaml
    /// </summary>
    public partial class GuestProjectsControl : UserControl
    {
        public GuestProjectsControl(GuestProjects_ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ParentWnd = this;
        }
    }
}
