using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Data;
using Services;

namespace WebClient.Services
{
    public class ProjectService : IClientProjectService
    {
        private readonly IDatabase database;

        public ProjectService(IDatabase database)
        {
            this.database = database;
        }

        public bool Add(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            return database.AddProject(model, contentType, fileStream, fileName);
        }

        public void Delete(int id)
        {
            database.DeleteProject(id);
        }

        public bool Edit(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            return database.EditProject(model, contentType, fileStream, fileName);
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
