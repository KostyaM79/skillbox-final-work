﻿using Microsoft.EntityFrameworkCore;
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
using Services;

namespace WebApiService.Services
{
    public class ProjectService : IApiProjectService
    {
        private AppDbContext context;

        public ProjectService(AppDbContext context)
        {
            this.context = context;
        }

        public bool Add(string title, string descr, string imgFileName)
        {
            Project project = new Project()
            {
                ProjectCaption = title,
                ProjectDescription = descr,
                ProjectImageFileName = imgFileName
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

            //foreach (Project p in projects)
            //{
            //    projectModels.Add(new ProjectModel()
            //    {
            //        Id = p.Id,
            //        ProjectTitle = p.ProjectCaption,
            //        ProjectDescr = p.ProjectDescription,
            //        ProjectImageFileName = p.ProjectImageFileName
            //    });
            //}

            //return projectModels.ToArray();
        }

        public bool Edit(int id, string title, string descr, Stream stream = null, string fileName = null)
        {
            string newFileName = null;

            Project project = context.Projects.FirstOrDefault(e => e.Id == id);             // Получаем проект из БД

            // Если картинка была изменена, то удаляем старую картинку и сохраняем новую
            if (stream != null)
            {
                File.Delete($"img\\projects-images\\{project.ProjectImageFileName}");       // Удаляем старую картинку
                newFileName = CreateFileName(fileName);                                     // Формируем имя нового файла
                using FileStream fs = File.Create($"img\\projects-images\\{newFileName}");  // Создаём поток для сохранения новой картинки
                stream.CopyTo(fs);                                                          // Копируем картинку в поток
                stream.Close();
                fs.Close();
            }

            project.ProjectCaption = title;
            project.ProjectDescription = descr;
            if(newFileName != null) project.ProjectImageFileName = newFileName;

            return context.SaveChanges() > 0;
        }

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

        public void Delete(int id)
        {
            Project project = context.Projects.FirstOrDefault(e => e.Id == id);
            File.Delete($"img\\projects-images\\{project.ProjectImageFileName}");
            context.Remove(project);
            context.SaveChanges();
        }
    }
}
