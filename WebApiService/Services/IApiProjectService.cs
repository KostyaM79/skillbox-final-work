using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public interface IApiProjectService : IProjectsService
    {
        bool Add(string title, string descr, IFormFile file);

        bool Edit(int id, string title, string descr, IFormFile file);
    }
}
