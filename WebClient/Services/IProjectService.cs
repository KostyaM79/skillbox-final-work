using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace WebClient.Services
{
    public interface IProjectService
    {
        bool Add(ProjectModel model, string contentType, Stream fileStream, string fileName);

        ProjectModel[] GetAll();

        ProjectModel Get(int id);

        bool Edit(ProjectModel model, string contentType, Stream fileStream, string fileName);
    }
}
