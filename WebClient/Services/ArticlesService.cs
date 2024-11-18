using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Services;
using System.IO;
using WebClient.Data;

namespace WebClient.Services
{
    public class ArticlesService : IClientArticlesService
    {
        private readonly IDatabase database;

        public ArticlesService(IDatabase database)
        {
            this.database = database;
        }

        public void Create(ArticleModel model, string contentType, Stream fileStream, string fileName)
        {
            database.AddArticle(model, contentType, fileStream, fileName);
        }

        public void Delete(int id)
        {
            database.DeleteArticle(id);
        }

        public ArticleModel Find(int id)
        {
            return database.FindArticle(id);
        }

        public ArticleModel[] GetAll()
        {
            return database.GetAllArticles();
        }

        public void Update(ArticleModel model, string contentType, Stream stream, string fileName)
        {
            database.UpdateArticle(model, contentType, stream, fileName);
        }
    }
}
