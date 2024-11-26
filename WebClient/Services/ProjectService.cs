using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Data;
using WebClient.Services;

namespace WebClient.Services
{
    /// <summary>
    /// Управляет проектами
    /// </summary>
    public class ProjectService : IClientProjectService
    {
        private readonly IDatabase database;

        public ProjectService(IDatabase database)
        {
            this.database = database;
        }

        public bool Add(ProjectModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            return database.AddProject(model, contentType, fileStream, fileName, token);
        }

        public void Delete(int id, string token)
        {
            database.DeleteProject(id, token);
        }

        public bool Edit(ProjectModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            return database.EditProject(model, contentType, fileStream, fileName, token);
        }

        public ProjectModel Get(int id)
        {
            return database.GetProject(id);
        }

        public ProjectModel[] GetAll()
        {
            ProjectModel[] model = database.GetAllProjects();
            return model;
        }
    }
}
