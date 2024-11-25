using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Models;
using WebApiService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebApiService.Services
{
    public class ArticleService : IApiArticlesService
    {
        private readonly AppDbContext context;

        public ArticleService(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(string title, string text, IFormFile file)
        {
            ImageManager im = new ImageManager();
            string imgFileName = im.SaveImage("articles-images", file);

            Article article = new Article()
            {
                ArticleCaption = title,
                ArticleText = text,
                ArticlePublishDate = DateTime.Now,
                ArticleImageFileName = imgFileName
            };

            context.Add(article);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Article article = context.Articles.FirstOrDefault(e => e.Id == id);
            File.Delete($"img\\articles-images\\{article.ArticleImageFileName}");
            context.Remove(article);
            context.SaveChanges();
        }

        public ArticleModel Find(int id)
        {
            Article article = context.Articles.AsNoTracking().FirstOrDefault(e => e.Id == id);
            return new ArticleModel()
            {
                Id = article.Id,
                ArticleCaption = article.ArticleCaption,
                ArticleText = article.ArticleText,
                ArticlePublishDate = article.ArticlePublishDate,
                ArticleImageFileName = article.ArticleImageFileName
            };
        }

        public ArticleModel[] GetAll()
        {
            return context.Articles.AsNoTracking().Select(e => new ArticleModel()
            {
                Id = e.Id,
                ArticleCaption = e.ArticleCaption,
                ArticleText = e.ArticleText,
                ArticlePublishDate = e.ArticlePublishDate,
                ArticleImageFileName = e.ArticleImageFileName
            }).ToArray();
        }

        public void Update(ArticleModel model, string contentType, Stream stream, string fileName)
        {
            Article article = context.Articles.FirstOrDefault(e => e.Id == model.Id);
            if (stream != null)
            {
                ImageManager im = new ImageManager();
                article.ArticleImageFileName = im.Update(article.ArticleImageFileName, stream, fileName, "articles-images");
            }

            article.ArticleCaption = model.ArticleCaption;
            article.ArticleText = model.ArticleText;

            if (!string.IsNullOrEmpty(model.ArticleImageFileName))
                article.ArticleImageFileName = model.ArticleImageFileName;

            context.SaveChanges();
        }
    }
}
