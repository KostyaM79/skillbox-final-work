using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DesktopClient.Services;
using System.Windows.Input;
using Models;
using System.Windows.Media.Imaging;
using DesktopClient.Dialogs;
using DesktopClient.General;
using Microsoft.Win32;
using System.IO;

namespace DesktopClient.ViewModels
{
    public class ProjectDialog_ViewModel : INotifyPropertyChanged
    {
        private readonly IDesktopProjectsService service;
        private ProjectModel project;
        private string filePath;
        private string fileName;
        private string fileExt;
        private EditProjectDialog parentWnd;
        private BitmapImage projectImage;

        public ProjectDialog_ViewModel(IDesktopProjectsService service)
        {
            this.service = service;
        }

        public EditProjectDialog ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                parentWnd.imgFile.MouseLeftButtonDown += OnMouseClick;
            }
        }

        public ProjectModel Project
        {
            get => project;
            set
            {
                project = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Project)));
            }
        }

        public string Title { get; set; }

        public string Descr { get; set; }

        public BitmapImage ProjectImage
        {
            get => projectImage;
            set
            {
                projectImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectImage)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public async void EditMode(int id)
        //{
        //    project = await service.GetAsync(id);
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri(project.ProjectImageFileName);
        //    bitmap.EndInit();
        //    ProjectImage = bitmap;
        //}

        public void AddMode()
        {
            ParentWnd.saveBtn.Command = new RelayCommand(Add); BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = File.OpenRead("..\\..\\..\\icons\\upload.png");
            bitmap.EndInit();
            ProjectImage = bitmap;
        }

        public void EditMode(ProjectModel project)
        {
            this.project = project;
            ProjectImage = CreateImage(project.ProjectImageFileName);

            Title = project.ProjectTitle;
            Descr = project.ProjectDescr;

            ParentWnd.saveBtn.Command = new RelayCommand(Edit);
        }

        private BitmapImage CreateImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            return bitmap;
        }

        private void Edit(object o)
        {
            ProjectModel proj = new ProjectModel()
            {
                Id = project.Id,
                ProjectTitle = Title,
                ProjectDescr = Descr
            };

            FileStream fs = !string.IsNullOrEmpty(filePath) ? File.OpenRead(filePath) : null;
            service.Update(proj, $"image/{fileExt}", fs, fileName);

            ParentWnd.DialogResult = true;
            ParentWnd.Close();
        }

        private void Add(object o)
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Descr) && !string.IsNullOrEmpty(filePath))
            {
                ProjectModel proj = new ProjectModel()
                {
                    ProjectTitle = Title,
                    ProjectDescr = Descr
                };

                service.Add(proj, $"image/{fileExt}", File.OpenRead(filePath), fileName);

                ParentWnd.DialogResult = true;
                ParentWnd.Close();
            }
        }

        private void OnMouseClick(object sender, MouseButtonEventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            if (dialog.ShowDialog().Value)
            {
                filePath = dialog.FileName;
                fileName = dialog.SafeFileName;
                fileExt = dialog.DefaultExt;
                if(!string.IsNullOrEmpty(filePath)) ProjectImage = CreateImage(filePath);
            }
        }
    }
}
