using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Services;
using Models;

namespace WebClient.Services
{
    public interface IClientArticlesService : IArticleService
    {
        void Create(ArticleModel model, string contentType, Stream fileStream, string fileName);
    }
}
