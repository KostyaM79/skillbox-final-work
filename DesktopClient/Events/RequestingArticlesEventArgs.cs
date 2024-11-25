using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class RequestingArticlesEventArgs
    {
        public RequestingArticlesEventArgs(ArticleModel model)
        {
            Article = model;
        }

        public ArticleModel Article
        {
            get;
            private set;
        }
    }
}
