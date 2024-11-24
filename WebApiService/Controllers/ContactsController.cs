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
    public class ContactsController : ControllerBase
    {
        private IApiSocialsService service;

        public ContactsController(IApiSocialsService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public void Create([FromForm] SocialModel model)
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;

            string link = HttpContext.Request.Form["Link"];

            service.Create(link, files[0]);
        }

        [HttpGet]
        [Route(nameof(ReadAll))]
        public IActionResult ReadAll()
        {
            return Ok(service.GetAll());
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }

        [HttpPost]
        [Route(nameof(Update))]
        public void Update([FromForm] SocialModel model)
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
