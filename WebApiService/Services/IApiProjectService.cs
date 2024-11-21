using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Services
{
    public interface IApiProjectService : IProjectsService
    {
        bool Add(string title, string descr, string fileName);

        bool Edit(int id, string title, string descr, Stream stream, string fileName);
    }
}
