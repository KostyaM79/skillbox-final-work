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
using System.IO;

namespace DesktopClient.ViewModels
{
    public class ProjectsControl_ViewModel
    {
        private object locker = new object();

        private ProjectsControl parentWnd;
        public event RequestingProjects_EventHandler ProjectsReceived;

        private readonly IDesktopProjectsService service;
        private ProjectModel[] projects;

        public ProjectsControl_ViewModel(IDesktopProjectsService service)
        {
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
                    ProjectControlBuilder constructor = new ProjectControlBuilder();
                    ParentWnd.projectsWrapPanel.Children.Clear();

                    foreach (ProjectModel t in args.Projects)
                    {
                        constructor.AddImage(t.ProjectImageFileName)
                            .AddEditBtn(new RelayCommand(EditAction), File.OpenRead("..\\..\\..\\icons\\edit.png"), t.Id)
                            .AddDeleteBtn(new RelayCommand(DeleteAction), File.OpenRead("..\\..\\..\\icons\\delete.png"), t.Id)
                            .AddTitle(t.ProjectTitle);

                        ParentWnd.projectsWrapPanel.Children.Add(constructor.Build());
                    }
                }
            }
        }

        private void EditAction(object o)
        {
            ProjectDialog_ViewModel viewModel = new ProjectDialog_ViewModel(ServiceFactory.GetService<IDesktopProjectsService>());
            EditProjectDialog dialog = new EditProjectDialog(viewModel);
            //viewModel.EditMode((int)(o as Button).Tag);
            viewModel.EditMode(projects.FirstOrDefault(e => e.Id == (int)(o as Button).Tag));
            if (dialog.ShowDialog().Value)
                GetDataAsync();
        }

        private void DeleteAction(object o)
        {

        }
    }
}
