using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;

namespace WebApiService.Services
{
    /// <summary>
    /// Управляет ссылками на соц сети
    /// </summary>
    public class SocialsService : IApiSocialsService
    {
        private AppDbContext context;

        public SocialsService(AppDbContext context)
        {
            this.context = context;
        }

        public SocialModel[] GetAll()
        {
            Socials[] socials = context.Socials.ToArray();      // Достаём все ссылки из БД
            SocialModel[] models = socials.Select(e => new SocialModel()
            {
                Id = e.Id,
                Link = e.Link,
                FileName = $"api/Images/dir/icons/file/{e.ImageFileName}"
            }).ToArray();

            return models;
        }

        /// <summary>
        /// Обновляет соц. сети в БД
        /// </summary>
        /// <param name="links"></param>
        /// <param name="files"></param>
        public void Update(string[] links, IFormFileCollection files)
        {
            ImageManager imageManager = new ImageManager();

            // Удаляем все иконки из папки
            Socials[] socials = context.Socials.ToArray();
            foreach (Socials s in socials)
                imageManager.DeleteImage("icons", s.ImageFileName);

            // Удаляем все ссылки из БД
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Socials]");

            // Сохраняем новые иконки в папку и создаём модели для сохранения в БД
            List<SocialModel> models = new List<SocialModel>();
            for (int i = 0; i < links.Length; i++)
            {
                SocialModel model = new SocialModel();
                model.FileName = imageManager.SaveImage("icons", files[i]);
                model.Link = links[i];
                models.Add(model);
            }


            List<Socials> socials1 = new List<Socials>();

            // Сохраняем новые ссылки в БД
            foreach (SocialModel model in models)
            {
                socials1.Add(new Socials()
                {
                    Link = model.Link,
                    ImageFileName = model.FileName
                });
            }

            context.AddRange(socials1);
            context.SaveChanges();
        }
    }
}
