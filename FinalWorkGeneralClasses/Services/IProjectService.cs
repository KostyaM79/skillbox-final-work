using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace Services
{
    public interface IProjectService
    {
        ProjectModel[] GetAll();

        ProjectModel Get(int id);

        void Delete(int id);
    }
}
