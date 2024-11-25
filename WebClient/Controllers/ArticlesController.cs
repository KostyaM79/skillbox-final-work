using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Services;
using Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IClientArticlesService service;
        private readonly IConfiguration configuration;

        public ArticlesController(IClientArticlesService service, IConfiguration configuration)
        {
            this.service = service;
            this.configuration = configuration;
        }

        public IActionResult ReadAll()
        {
            ArticleModel[] articles = service.GetAll();

            foreach (ArticleModel m in articles)
            {
                m.ModifyFileName(configuration["ApiLocation"]);
            }

            return View(articles);
        }

        public IActionResult Find(int id)
        {
            ArticleModel article = service.Find(id);
            article.ModifyFileName(configuration["ApiLocation"]);
            return View(article);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ArticleModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];

            service.Create(
                model,
                HttpContext.Request.Form.Files[0].ContentType,
                HttpContext.Request.Form.Files[0].OpenReadStream(),
                HttpContext.Request.Form.Files[0].FileName,
                token);

            return Redirect("/Articles/ReadAll");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            service.Delete(id, token);
            return Redirect("/Articles/ReadAll");
        }

        [Authorize]
        public IActionResult Update(ArticleModel model)
        {
            string token = HttpContext.Request.Cookies["jwt"];
            IFormFileCollection files = HttpContext.Request.Form.Files;
            service.Update(
                model,
                files.Any() ? files[0].ContentType : null,
                files.Any() ? files[0].OpenReadStream() : null,
                files.Any() ? files[0].FileName : null,
                token
                );
            return Redirect("/Articles/ReadAll");
        }
    }
}
