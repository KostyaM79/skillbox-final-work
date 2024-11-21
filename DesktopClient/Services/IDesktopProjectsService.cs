using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Models;
using System.IO;

namespace DesktopClient.Services
{
    public interface IDesktopProjectsService : IProjectsService
    {

        void Add(ProjectModel model, string contentType, Stream fileStream, string fileName);
        Task<ProjectModel[]> GetAllAsync();

        Task<ProjectModel> GetAsync(int id);

        void Update(ProjectModel model, string contentType, Stream fileStream, string fileName);
    }
}
