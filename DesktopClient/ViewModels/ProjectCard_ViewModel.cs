using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DesktopClient.General;
using DesktopClient.UserControls;
using Models;

namespace DesktopClient.ViewModels
{

    public class ProjectCard_ViewModel
    {
        private ProjectViewCard parentWnd;
        private ProjectModel model;

        public static ProjectCard_ViewModel CreateProjectCard(ProjectModel model, RelayCommand editAction = null, RelayCommand deleteAction = null)
        {
            ProjectCard_ViewModel viewModel = CreateProjectCard(model);
            FillData(viewModel, model, editAction, deleteAction);
            return viewModel;
        }

        private static ProjectCard_ViewModel CreateProjectCard(ProjectModel model)
        {
            ProjectViewCard control = new ProjectViewCard();
            ProjectCard_ViewModel viewModel = new ProjectCard_ViewModel(model);
            control.DataContext = viewModel;
            viewModel.ParentWnd = control;
            return viewModel;
        }

        private static void FillData(ProjectCard_ViewModel viewModel, ProjectModel model, RelayCommand editAction, RelayCommand deleteAction)
        {
            if (editAction != null)
            {
                Button btn1 = Buttons.GetEditButton(editAction, model.Id);
                btn1.CommandParameter = viewModel.ParentWnd;
                viewModel.ParentWnd.btns.Children.Add(btn1);
            }

            if (deleteAction != null)
            {
                Button btn2 = Buttons.GetDeleteButton(deleteAction, model.Id);
                btn2.CommandParameter = viewModel.ParentWnd;
                viewModel.ParentWnd.btns.Children.Add(btn2);
            }
        }


        public ProjectCard_ViewModel(ProjectModel model)
        {
            this.model = model;
            Id = model.Id;

            Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(model.ProjectImageFileName);
            Image.EndInit();

            Title = model.ProjectTitle;
        }

        public ProjectViewCard ParentWnd
        {
            get => parentWnd;
            set => parentWnd = value;
        }

        public ProjectModel Model => model;

        public int Id { get; set; }

        public BitmapImage Image { get; set; }

        public string Title { get; set; }
    }
}
