using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Models;
using Services;
using DesktopClient.General;

namespace DesktopClient.Services
{
    public class ArticlesService : IDesktopArticlesService
    {
        private readonly Server server = Server.Create();

        public void Create(ArticleModel model, string contentType, Stream fileStream, string fileName)
        {
            server.AddArticle(model, contentType, fileStream, fileName);
        }

        public void Delete(int id)
        {
            server.DeleteArticle(id);
        }

        public ArticleModel Find(int id)
        {
            throw new NotImplementedException();
        }

        public ArticleModel[] GetAll()
        {
            ArticleModel[] articles = server.ReadAllArticles();
            foreach (ArticleModel tenp in articles)
                tenp.ModifyFileName(ConfigurationManager.AppSettings["api-location"]);
            return articles;
        }

        public void Update(ArticleModel model, string contentType, Stream stream, string fileNam)
        {
            server.UpdateArticle(model, contentType, stream, fileNam);
        }
    }
}
