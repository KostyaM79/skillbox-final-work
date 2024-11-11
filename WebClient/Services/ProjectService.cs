using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Data;

namespace WebClient.Services
{
    public class ProjectService : IProjectService
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
    }
}
