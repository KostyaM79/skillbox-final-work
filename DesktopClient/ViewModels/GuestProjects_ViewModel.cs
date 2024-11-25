using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using DesktopClient.Services;
using DesktopClient.UserControls;
using DesktopClient.General;
using DesktopClient.Dialogs;
using DesktopClient.Events;
using Models;
using System.IO;
using System.Windows.Controls;

namespace DesktopClient.ViewModels
{
    public class GuestProjects_ViewModel
    {
        private object locker = new object();

        private GuestProjectsControl parentWnd;
        public event RequestingProjects_EventHandler ProjectsReceived;

        private readonly IDesktopProjectsService service;
        private ProjectModel[] projects;

        public GuestProjects_ViewModel(IDesktopProjectsService service)
        {
            ProjectsReceived += OnRequestingProjects;
            this.service = service;
            GetDataAsync();
        }

        public GuestProjectsControl ParentWnd
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

        /// <summary>
        /// Получает проекты из БД
        /// </summary>
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
                            .AddTitle(t.ProjectTitle);

                        ParentWnd.projectsWrapPanel.Children.Add(constructor.Build());
                    }
                }
            }
        }
    }
}

