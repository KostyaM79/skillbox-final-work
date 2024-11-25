using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class RequestingProjectEventArgs : EventArgs
    {
        public RequestingProjectEventArgs(ProjectModel project)
        {
            Project = project;
        }

        public ProjectModel Project
        {
            get;
            private set;
        }
    }
}
