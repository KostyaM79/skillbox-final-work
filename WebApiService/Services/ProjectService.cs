using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebApiService.Data;
using Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using WebApiService.Services;
using Microsoft.AspNetCore.Http;

namespace WebApiService.Services
{
    public class ProjectService : IApiProjectService
    {
        private AppDbContext context;

        public ProjectService(AppDbContext context)
        {
            this.context = context;
        }

        public bool Add(string title, string descr, IFormFile file)
        {
            string newFileName = CreateFileName(file.FileName);
            SaveImage(file, newFileName);                            // Сохраняем изображение

            //Сохраняем данные в БД
            Project project = new Project()
            {
                ProjectCaption = title,
                ProjectDescription = descr,
                ProjectImageFileName = newFileName
            };

            context.Projects.Add(project);
            return context.SaveChanges() > 0;
        }

        public ProjectModel Get(int id)
        {
            Project project = context.Projects.AsNoTracking().FirstOrDefault(e => e.Id == id);
            return new ProjectModel()
            {
                Id = project.Id,
                ProjectTitle = project.ProjectCaption,
                ProjectDescr = project.ProjectDescription,
                ProjectImageFileName = project.ProjectImageFileName
            };
        }

        public ProjectModel[] GetAll()
        {
            Project[] projects = context.Projects.AsNoTracking().ToArray();     // Получаем проекты из базы данных
            List<ProjectModel> projectModels = new();                           // Создаём коллекцию для проектов

            return projects.Select(e => new ProjectModel()
            {
                Id = e.Id,
                ProjectTitle = e.ProjectCaption,
                ProjectDescr = e.ProjectDescription,
                ProjectImageFileName = e.ProjectImageFileName
            }).ToArray();
        }

        public bool Edit(int id, string title, string descr, IFormFile file = null)
        {
            string newFileName = null;

            Project project = context.Projects.FirstOrDefault(e => e.Id == id);             // Получаем проект из БД

            // Если картинка была изменена, то удаляем старую картинку и сохраняем новую
            if (file != null)
            {
                //File.Delete($"img\\projects-images\\{project.ProjectImageFileName}");       // Удаляем старую картинку
                DeleteImage(project.ProjectImageFileName);
                newFileName = CreateFileName(file.FileName);                                  // Формируем имя нового файла
                SaveImage(file, newFileName);

                //using FileStream fs = File.Create($"img\\projects-images\\{newFileName}");  // Создаём поток для сохранения новой картинки
                //stream.CopyTo(fs);                                                          // Копируем картинку в поток
                //stream.Close();
                //fs.Close();
            }

            project.ProjectCaption = title;
            project.ProjectDescription = descr;
            if(newFileName != null) project.ProjectImageFileName = newFileName;

            return context.SaveChanges() > 0;
        }

        public void Delete(int id)
        {
            Project project = context.Projects.FirstOrDefault(e => e.Id == id);
            //File.Delete($"img\\projects-images\\{project.ProjectImageFileName}");
            DeleteImage(project.ProjectImageFileName);
            context.Remove(project);
            context.SaveChanges();
        }

        /// <summary>
        /// Сохраняет изображение
        /// </summary>
        /// <param name="file"></param>
        private void SaveImage(IFormFile file, string fileName)
        {
            DirectoryInfo dir = Directory.CreateDirectory("img\\projects-images");

            string path = $"{dir.FullName}\\{fileName}";
            using FileStream fs = File.Create(path);
            using Stream stream = file.OpenReadStream();
            stream.CopyTo(fs);
            fs.Close();
            stream.Close();
        }

        /// <summary>
        /// Удаляет изображение из папки projects-images
        /// </summary>
        /// <param name="fileName"></param>
        private void DeleteImage(string fileName)
        {
            File.Delete($"img\\projects-images\\{fileName}");       // Удаляем файл
        }

        /// <summary>
        /// Создаёт уникальное имя файла
        /// </summary>
        /// <param name="srcFileName"></param>
        /// <returns></returns>
        private string CreateFileName(string srcFileName)
        {
            Regex regex = new Regex("(\\..+)$");
            string ext = regex.Match(srcFileName).Value;
            string str = $"{srcFileName}{DateTime.Now}";
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = MD5.Create().ComputeHash(bytes);
            string[] ss = hash.Select(e => $"{e:X2}").ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (string s in ss)
                sb.Append(s);
            sb.Append(ext);
            return sb.ToString();
        }
    }
}
