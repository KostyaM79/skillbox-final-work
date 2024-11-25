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

        public void Create(ArticleModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            database.AddArticle(model, contentType, fileStream, fileName, token);
        }

        public void Delete(int id, string token)
        {
            database.DeleteArticle(id, token);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ArticleModel Find(int id)
        {
            return database.FindArticle(id);
        }

        public ArticleModel[] GetAll()
        {
            return database.GetAllArticles();
        }

        public ArticleModel Get(int id)
        {
            return database.FindArticle(id);
        }

        public void Update(ArticleModel model, string contentType, Stream stream, string fileName, string token)
        {
            database.UpdateArticle(model, contentType, stream, fileName, token);
        }

        public void Update(ArticleModel model, string contentType, Stream stream, string fileNam)
        {
            throw new NotImplementedException();
        }
    }
}
