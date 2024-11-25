using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DesktopClient.UserControls;
using System.ComponentModel;
using Models;

namespace DesktopClient.ViewModels
{
    public class ViewProject_ViewModel : INotifyPropertyChanged
    {
        private ViewProjectControl parentWnd;
        private ProjectModel model;
        private BitmapImage image;

        public event PropertyChangedEventHandler PropertyChanged;

        public static ViewProject_ViewModel Create(ProjectModel model)
        {
            ViewProject_ViewModel viewModel = new ViewProject_ViewModel(model);
            viewModel.ParentWnd = new ViewProjectControl();
            viewModel.ParentWnd.DataContext = viewModel;
            return viewModel;
        }

        public ViewProject_ViewModel(ProjectModel model)
        {
            this.model = model;
        }

        public ViewProjectControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;

                Image = new BitmapImage();
                Image.BeginInit();
                Image.UriSource = new Uri(model.ProjectImageFileName);
                Image.EndInit();
            }
        }

        public string Title => model.ProjectTitle;

        public string Descr => model.ProjectDescr;

        public BitmapImage Image
        {
            get => image;
            set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }
    }
}
