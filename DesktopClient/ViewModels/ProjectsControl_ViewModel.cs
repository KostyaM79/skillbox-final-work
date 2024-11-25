using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using DesktopClient.UserControls;
using DesktopClient.Services;
using DesktopClient.General;
using Models;
using DesktopClient.Events;
using System.Windows.Media.Imaging;
using System.Windows;
using DesktopClient.Dialogs;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class ProjectsControl_ViewModel
    {
        private object locker = new object();
        private bool viewMode;
        private RelayCommand addProjectCmd;

        private ProjectsControl parentWnd;
        public event RequestingProjects_EventHandler ProjectsReceived;
        public event RequestingProjectEventHandler RequestingProject;

        private readonly IDesktopProjectsService service;
        private ProjectModel[] projects;

        public static ProjectsControl_ViewModel Create(IDesktopProjectsService service, bool viewMode = false)
        {
            ProjectsControl_ViewModel viewModel = new ProjectsControl_ViewModel(service, viewMode);
            ProjectsControl control = new ProjectsControl(viewModel);
            if (viewMode) control.content.Children.Remove(control.addBtn);
            return viewModel;
        }

        public ProjectsControl_ViewModel(IDesktopProjectsService service, bool viewMode = false)
        {
            this.viewMode = viewMode;
            ProjectsReceived += OnRequestingProjects;
            this.service = service;
            GetDataAsync();
        }

        public ProjectsControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                ProjectsReceived?.Invoke(new RequestingProjectsEventArgs(projects));
            }
        }

        public ProjectModel[] Projects
        {
            get => projects;
            set
            {
                projects = value;
                ProjectsReceived?.Invoke(new RequestingProjectsEventArgs(projects));
            }
        }

        private async void GetDataAsync()
        {
            ProjectModel[] projects = await service.GetAllAsync();

            if (projects != null)
            {
                lock (locker)
                    Projects = projects;
            }
        }

        private void OnRequestingProjects(RequestingProjectsEventArgs args)
        {
            if (ParentWnd != null)
            {
                lock (locker)
                {
                    ParentWnd.projects.Children.Clear();

                    foreach (ProjectModel temp in projects)
                    {
                        ProjectCard_ViewModel projVm;

                        if (viewMode)
                        {
                            projVm = ProjectCard_ViewModel.CreateProjectCard(temp);
                        }
                        else
                        {
                            projVm = ProjectCard_ViewModel.CreateProjectCard(temp, new RelayCommand(EditAction), new RelayCommand(DeleteAction));
                        }

                        projVm.ParentWnd.projectLnk.MouseLeftButtonDown += OnProjectCardClick;
                        ParentWnd.projects.Children.Add(projVm.ParentWnd);
                    }
                }
            }
        }

        private void EditAction(object o)
        {
            ProjectViewCard control = o as ProjectViewCard;
            ProjectCard_ViewModel viewModel = control.DataContext as ProjectCard_ViewModel;

            ProjectDialog_ViewModel dialogViewModel = new ProjectDialog_ViewModel(service);
            EditProjectDialog dialog = new EditProjectDialog(dialogViewModel);
            //viewModel.EditMode(projects.FirstOrDefault(e => e.Id == (int)(o as Button).Tag));
            dialogViewModel.EditMode(viewModel);
            if (dialog.ShowDialog().Value)
                GetDataAsync();
        }

        private void DeleteAction(object o)
        {
            ProjectViewCard control = o as ProjectViewCard;
            ProjectCard_ViewModel viewModel = control.DataContext as ProjectCard_ViewModel;

            service.Delete(viewModel.Id);
            GetDataAsync();
        }

        private void AddProject()
        {
            ProjectDialog_ViewModel viewModel = new ProjectDialog_ViewModel(ServiceFactory.GetService<IDesktopProjectsService>());
            EditProjectDialog dialog = new EditProjectDialog(viewModel);
            viewModel.AddMode();
            if (dialog.ShowDialog().Value)
                GetDataAsync();
        }

        public RelayCommand AddProject_Cmd
        {
            get
            {
                return addProjectCmd ?? (addProjectCmd = new RelayCommand(obj =>
                {
                    AddProject();
                }));
            }
        }

        private void OnProjectCardClick(object sender, MouseButtonEventArgs args)
        {
            ProjectViewCard card = ((sender as StackPanel).Parent as Grid).Parent as ProjectViewCard;
            RequestingProject?.Invoke(this, new RequestingProjectEventArgs((card.DataContext as ProjectCard_ViewModel).Model));
        }
    }
}
