using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class ArticlesReceivedEventArgs : EventArgs
    {
        public ArticlesReceivedEventArgs(ArticleModel[] articles)
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
