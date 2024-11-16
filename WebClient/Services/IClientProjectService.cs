using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;

namespace Services
{
    public interface IClientProjectService : IProjectService
    {
        bool Add(ProjectModel model, string contentType, Stream fileStream, string fileName);

        bool Edit(ProjectModel model, string contentType, Stream fileStream, string fileName);
    }
}
