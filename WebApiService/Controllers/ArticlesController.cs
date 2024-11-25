using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using WebApiService.Services;
using Models;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IApiArticlesService service;

        public ArticlesController(IApiArticlesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            ArticleModel[] models = service.GetAll();
            if (models != null) return Ok(models);
            else return StatusCode(500, new { Message = "Не удалось получить список статей из БД!" });
        }

        [HttpGet]
        [Route("Read/{id:int}")]
        public IActionResult Read(int id)
        {
            return Ok(service.Find(id));
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create([FromForm] ArticleModel model)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;

            string title = HttpContext.Request.Form["ArticleTitle"];
            string text = HttpContext.Request.Form["ArticleText"];

            service.Create(title, text, files[0]);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("Update")]
        public void Update([FromForm] ArticleModel model)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            string fileName = null;
            string contentType = null;
            Stream stream = null;

            if (files.Any())
            {
                fileName = files[0].FileName;
                contentType = files[0].ContentType;
                stream = files[0].OpenReadStream();
            }

            service.Update(model, contentType, stream, fileName);
        }
    }
}
