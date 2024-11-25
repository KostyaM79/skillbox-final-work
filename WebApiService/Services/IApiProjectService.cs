using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Models;

namespace WebApiService.Services
{
    public interface IApiProjectService
    {
        ProjectModel[] GetAll();

        ProjectModel Get(int id);

        void Delete(int id);

        bool Add(string title, string descr, IFormFile file);

        bool Edit(int id, string title, string descr, IFormFile file);
    }
}
