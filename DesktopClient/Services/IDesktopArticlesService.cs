using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using System.IO;

namespace DesktopClient.Services
{
    public interface IDesktopArticlesService : IArticleService
    {
        void Create(ArticleModel model, string contentType, Stream fileStream, string fileName);
    }
}
