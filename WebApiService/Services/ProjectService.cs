using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebApiService.Data;
using WebApiService.Models;

namespace WebApiService.Services
{
    public class ProjectService : IProjectService
    {
        private AppDbContext context;

        public ProjectService(AppDbContext context)
        {
            this.context = context;
        }

        public int Add(string title, string descr, string imgFileName)
        {
            Project project = new Project()
            {
                ProjectCaption = title,
                ProjectDescription = descr,
                ProjectImageFileName = imgFileName
            };

            context.Projects.Add(project);
            context.SaveChanges();

            return project.Id;
        }

        public ProjectModel[] GetAll()
        {
            Project[] projects = context.Projects.AsNoTracking().ToArray();

            List<ProjectModel> projectModels = new();
            foreach (Project p in projects)
            {
                projectModels.Add(new ProjectModel()
                {
                    ProjectCaption = p.ProjectCaption,
                    ProjectDescription = p.ProjectDescription,
                    ImageFileName = p.ProjectImageFileName
                });
            }

            return projectModels.ToArray();
        }
    }
}
