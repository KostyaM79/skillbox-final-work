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
        public ArticleModel Get(int id);

        void Create(ArticleModel model, string contentType, Stream fileStream, string fileName, string token);

        void Update(ArticleModel model, string contentType, Stream stream, string fileName, string token);

        void Delete(int id, string token);
    }
}
