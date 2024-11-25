using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Services;
using System.ComponentModel;
using System.Windows.Controls;
using DesktopClient.UserControls;
using DesktopClient.General;
using DesktopClient.Events;

namespace DesktopClient.ViewModels
{
    public class Guest_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly UserControl startControl = new GuestCanvas();
        private UserControl content;

        private RelayCommand mainCmd;
        private RelayCommand projectsCmd;
        private RelayCommand servicesCmd;
        private RelayCommand blogCmd;
        private RelayCommand contactsCmd;

        public Guest_ViewModel()
        {
            GuestMain_ViewModel viewModel = new GuestMain_ViewModel(ServiceFactory.GetService<IDesktopOrdersService>());
            content = new GuestMainControl(viewModel);
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




        public RelayCommand Main_Cmd
        {
            get
            {
                return mainCmd ?? (mainCmd = new RelayCommand(obj =>
                {
                    GuestMain_ViewModel viewModel = new GuestMain_ViewModel(ServiceFactory.GetService<IDesktopOrdersService>());
                    ContentControl = new GuestMainControl(viewModel);
                }));
            }
        }

        public RelayCommand Projects_Cmd
        {
            get
            {
                return projectsCmd ?? (projectsCmd = new RelayCommand(obj =>
                {
                    ProjectsControl_ViewModel viewModel = ProjectsControl_ViewModel.Create(ServiceFactory.GetService<IDesktopProjectsService>(), true);
                    viewModel.RequestingProject += OnRequestingProject;
                    ContentControl = viewModel.ParentWnd;
                }));
            }
        }

        public RelayCommand Services_Cmd
        {
            get
            {
                return servicesCmd ?? (servicesCmd = new RelayCommand(obj =>
                {
                    ServicesControl_ViewModel viewModel = new ServicesControl_ViewModel(ServiceFactory.GetService<IDesktopServicesService>(), true);
                    ServicesControl control = new ServicesControl(viewModel);
                    control.contentGrid.Children.Remove(control.addBtn);
                    ContentControl = control;
                }));
            }
        }

        public RelayCommand Blog_Cmd
        {
            get
            {
                return blogCmd ?? (blogCmd = new RelayCommand(obj =>
                {
                    Blog_ViewModel viewModel = new Blog_ViewModel(ServiceFactory.GetService<IDesktopArticlesService>(), true);
                    viewModel.RequestingArticle += OnRequestingArticle;
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
                    ContentControl = Contacts_VievModel.CreateContectsControl(ServiceFactory.GetService<IDesktopSocialsService>(), true).ParentWnd;
                }));
            }
        }

        private void OnRequestingProject(object sender, RequestingProjectEventArgs args)
        {
            ContentControl = ViewProject_ViewModel.Create(args.Project).ParentWnd;
        }

        private void OnRequestingArticle(object sender, RequestingArticlesEventArgs args)
        {
            ContentControl = ViewArticle_ViewModel.Create(args.Article).ParentWnd;
        }
    }
}
