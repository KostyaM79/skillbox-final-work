using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using DesktopClient.General;
using Models;
using System.IO;

namespace DesktopClient.Services
{
    public class ProjectsService : IDesktopProjectsService
    {
        private readonly Server server = new Server();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectModel[] GetAll()
        {
            return server.GetAllProjects();
        }

        public async Task<ProjectModel[]> GetAllAsync()
        {
            ProjectModel[] projects = await server.GetAllProjectsAsync();
            foreach (ProjectModel t in projects)
                t.ModifyFileName(ConfigurationManager.AppSettings["api-location"]);
            return projects;
        }

        public async Task<ProjectModel> GetAsync(int id)
        {
            ProjectModel project = await server.GetProjectAsync(id);
            if (project != null)
                project.ModifyFileName(ConfigurationManager.AppSettings["api-location"]);
            return project;
        }

        public void Update(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            server.UpdateProject(model, contentType, fileStream, fileName);
        }
    }
}
