using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(ArticleModel[] articles)
        {
            Articles = articles;
        }

        public ArticleModel[] Articles
        {
            get;
            private set;
        }
    }
}
