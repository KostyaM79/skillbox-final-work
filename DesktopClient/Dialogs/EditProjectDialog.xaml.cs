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
using System.Windows.Shapes;
using DesktopClient.ViewModels;

namespace DesktopClient.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для EditProjectDialog.xaml
    /// </summary>
    public partial class EditProjectDialog : Window
    {
        public EditProjectDialog(ProjectDialog_ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ParentWnd = this;
        }
    }
}
