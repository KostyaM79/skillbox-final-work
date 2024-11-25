using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;

namespace WebClient.Services
{
    public interface IClientProjectService
    {
        ProjectModel[] GetAll();

        ProjectModel Get(int id);

        void Delete(int id, string token);

        bool Add(ProjectModel model, string contentType, Stream fileStream, string fileName, string token);

        bool Edit(ProjectModel model, string contentType, Stream fileStream, string fileName, string token);
    }
}
