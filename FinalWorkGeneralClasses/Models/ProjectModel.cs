using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProjectModel
    {
        public int Id { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectDescr { get; set; }

        public string ProjectImageFileName { get; set; }

        /// <summary>
        /// Модифицирует имя файла в ссылку
        /// </summary>
        /// <param name="baseAddress"></param>
        public void ModifyFileName(string baseAddress)
        {
            ProjectImageFileName = $"{baseAddress}api/Images/dir/projects-images/file/{ProjectImageFileName}";
        }
    }
}
