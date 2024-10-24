using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectCaption { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectImageFileName { get; set; }
    }
}
