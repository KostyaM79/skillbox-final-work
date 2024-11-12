using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace WebApiService.Services
{
    public interface IProjectService
    {
        ProjectModel Get(int id);

        ProjectModel[] GetAll();

        int Add(string title, string descr, string imgFileName);

        void Edit(int id, string title, string descr, Stream stream, string fileName);
    }
}
