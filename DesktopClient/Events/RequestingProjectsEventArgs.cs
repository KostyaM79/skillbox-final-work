using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class RequestingProjectsEventArgs : EventArgs
    {
        public RequestingProjectsEventArgs(ProjectModel[] projects)
        {
            Projects = projects;
        }

        public ProjectModel[] Projects
        {
            get;
            private set;
        }
    }
}
